using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace mongodb.Actions
{
    public class M99Quit : IMenuAction
    {
        public int Id => 99;
        public string Description => "Quit";
        public void Execute(IAppConfigurations configurations, AppMongoClient appMongoClient, ILogger logger)
        {
            Environment.Exit(0);
        }
    }
}
