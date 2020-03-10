using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using mongodb.Documents;
using MongoDB.Driver;

namespace mongodb.Actions
{
    public class M04Indexes : IMenuAction
    {
        public int Id => 4;
        public string Description => "Create Indexes";

        public void Execute(IAppConfigurations configurations, AppMongoClient appMongoClient, ILogger logger)
        {
            var collection = appMongoClient.MongoDatabase.GetCollection<SalesRecord>("SalesRecords");
            var regionKey = Builders<SalesRecord>.IndexKeys.Ascending(field => field.Region);
            var countryKey = Builders<SalesRecord>.IndexKeys.Ascending(field => field.Country);
            var keys = new IndexKeysDefinitionBuilder<SalesRecord>().Combine(new[]{regionKey, countryKey});
            var indexModel = new CreateIndexModel<SalesRecord>(keys);
            var result = collection.Indexes.CreateOne(indexModel);
            
            logger.LogInformation($"index result {result}");

            var indexes = collection.Indexes.List();
            indexes.ForEachAsync(x => logger.LogInformation($"{x.ToString()}"));

        }
    }
}
