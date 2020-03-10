using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using mongodb.Actions;

namespace mongodb
{
    public class AppInitializer
    {
        private readonly IAppConfigurations _configurations;
        private readonly AppMongoClient _appMongoClient;
        private readonly ILogger<AppInitializer> _logger;
        private readonly IEnumerable<IMenuAction> _actions;

        public AppInitializer(IAppConfigurations configurations, AppMongoClient appMongoClient, ILogger<AppInitializer> logger, IEnumerable<IMenuAction> actions)
        {
            _configurations = configurations;
            _appMongoClient = appMongoClient;
            _logger = logger;
            _actions = actions;
        }

        public void Start()
        {
            _logger.LogDebug("starting AppInitializer");
            bool terminate = false;

            do
            {
                foreach (var menuAction in _actions.OrderBy(x => x.Id))
                {
                    Console.WriteLine($"{menuAction.Id:##}  {menuAction.Description}");
                }

                var selection = 0;
                int.TryParse(Console.ReadLine(), out selection);
                Console.Clear();

                var action = _actions.FirstOrDefault(x => x.Id == selection);

                if (action == null)
                {
                    Console.WriteLine("Invalid selection. try again.");
                    continue;
                }

                action.Execute(_configurations, _appMongoClient, _logger);


            } while (true);

        }

    }

    public class Startup
    {
        static void Main(string[] args)
        {
            var serviceProvider = IoC();
            serviceProvider.GetService<AppInitializer>().Start();
        }

        private static ServiceProvider IoC()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IAppConfigurations, AppConfigurations>()
                .AddLogging(config => config.AddConsole())
                .AddSingleton(typeof(AppInitializer))
                .AddSingleton(typeof(AppMongoClient))
                .AddTransient<IMenuAction, M01RefreshSalesDataSet>()
                .AddTransient<IMenuAction, M99Quit>()
                .AddTransient<IMenuAction, M02ReadAllSalesData>()
                .AddTransient<IMenuAction, M03UpdateData>()
                .AddTransient<IMenuAction, M04Indexes>()
                .AddTransient<IMenuAction, M15DownloadAssemblies>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Startup>();
            logger.LogInformation("This is a test of the logger");

            return serviceProvider;
        }
    }
}
