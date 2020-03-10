using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace mongodb.Actions
{
    public interface IMenuAction
    {
        int Id { get;  }
        string Description { get; }
        void Execute(IAppConfigurations configurations, AppMongoClient appMongoClient, ILogger logger);
    }
}
