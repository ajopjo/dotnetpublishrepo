using System;
using System.Diagnostics.CodeAnalysis;
using Autofac.Extensions.DependencyInjection;
using CorrelationId;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using Microsoft.Extensions.Logging;
using RB.Api.Core.Extensions.Startup.Bootstrapping;
using RB.Api.Core.Extensions.Startup.Handlers;
using RB.Api.Core.Extensions.Startup.Swagger;
using RB.Core.Extensions.Startup.Environment;

namespace Inventory
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Autofac.IContainer ApplicationContainer { get; private set; }
        public IConfiguration Configuration { get; }
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _logger = loggerFactory.CreateLogger<Startup>();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks(checks => { checks.AddUrlCheck("http://bzo.bosch.com/bzo/en/start_page.html"); });
            services.AddBaseBootstrapping<Startup>();

            services.AddSwaggerServices(Configuration);
            //.AddNamedHttpClients(Configuration)
            //.AddLinkHelper();

            ApplicationContainer = services.AddAutofacContainer();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IApplicationLifetime applicationLifetime,
            IHostingEnvironment env)
        {
            app.UseCorrelationId(new CorrelationIdOptions
            {
                UseGuidForCorrelationId = true
            })
                .UseXfo(options => options.Deny())
                .UseXContentTypeOptions()
                .UseCsp(options => { options.StyleSources(o => o.Self()); })
                .UseXXssProtection(options => options.EnabledWithBlockMode());

            if (env.IsPrdDmzUs())
            {
                app.UseCsp(options => { options.DefaultSources(o => o.Self()); });
            }

            app.UseExceptionHandler(_logger)
                .UseSwagger(Configuration)
                .UseMvc();

            applicationLifetime.ApplicationStopped.Register(() => { ApplicationContainer.Dispose(); });
        }
    }
}