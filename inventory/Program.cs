using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using RB.Core.Extensions.Startup.Serilog;

namespace Inventory
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(server => server.AddServerHeader = false)
                .UseSerilog(options => options.VersionFromAssemblyContaining<Startup>())
                .UseStartup<Startup>()
                .Build();
    }
}