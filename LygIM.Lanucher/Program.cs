using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Mehdime.Entity;
using LygIM.Buiness;
using LygIM.Buiness.Services.Repository;
using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace LygIM.Lanucher {
	internal class InstanceIdentifier : MarshalByRefObject { }
	class Program {
		static void Main(string[] args) {

			CreateHostBuilder(args).Build().RunAsync();

			Console.ReadLine();
		}

		static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
			.ConfigureAppConfiguration((option) =>
			{

			})
			.ConfigureLogging((context, logging) =>
			{

				logging.AddFilter("System", LogLevel.Warning);
				logging.AddFilter("Microsoft", LogLevel.Warning);

			}).ConfigureServices((services) =>
			{
				services.AddHostedService<Startup>();
				services.AddTransient<IDbContextScopeFactory, DbContextScopeFactory>();
				services.AddTransient<IAmbientDbContextLocator, AmbientDbContextLocator>();
				services.AddTransient<IWorkspaceRepository, WorkspaceRepository>();

			}).UseMyNLog();

	}
}
