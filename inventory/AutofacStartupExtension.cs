using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory
{
    [ExcludeFromCodeCoverage]
    public static class AutofacStartupExtension
    {
        public static IContainer AddAutofacContainer(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyModules(assembly);

            return builder.Build();
        }
    }
}