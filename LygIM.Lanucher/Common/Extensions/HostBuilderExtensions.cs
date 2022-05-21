using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.Web;
using System.IO;

namespace LygIM.Lanucher {
	public static class HostBuilderExtensions {
		public static IHostBuilder UseMyNLog(this IHostBuilder builder) {
                        
            builder.ConfigureAppConfiguration((context, configuration) =>
            {
                var environment = context.HostingEnvironment;
                //environment.ConfigureNLog($"{environment.ContentRootPath}{Path.DirectorySeparatorChar}NLog.config");
                LogManager.Configuration.Variables["configDir"] = environment.ContentRootPath;

            });

            return builder;
		}
        //private static LoggingConfiguration ConfigureNLog(this IHostEnvironment env, string configFileRelativePath) {

        //    ConfigurationItemFactory.Default.RegisterItemsFromAssembly(typeof(HostBuilderExtensions).GetType().Assembly);
        //    LogManager.AddHiddenAssembly(typeof(HostBuilderExtensions).GetType().Assembly);
        //    var fileName = Path.Combine(env.ContentRootPath, configFileRelativePath);
        //    LogManager.LoadConfiguration(fileName);
        //    return LogManager.Configuration;
        //}
    }
}
