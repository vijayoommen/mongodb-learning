using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using mongodb.Documents;
using MongoDB.Driver;

namespace mongodb.Actions
{
    class M03UpdateData : IMenuAction
    {
        public int Id => 3;
        public string Description => "Update data";
        public void Execute(IAppConfigurations configurations, AppMongoClient appMongoClient, ILogger logger)
        {
            Console.WriteLine($"Region");
            var region = Console.ReadLine();
            Console.WriteLine("Country");
            var country = Console.ReadLine();
            Console.Write("Set OrderPriority as: ");
            var orderPriority = Console.ReadLine();

            var collection = appMongoClient.MongoDatabase.GetCollection<SalesRecord>("SalesRecords");
            
            // find records and update at the same time
            var filter = Builders<SalesRecord>.Filter.Where(x => x.Region == region && x.Country == country && x.OrderPriority == "M");
            var update = Builders<SalesRecord>.Update.Set(field => field.OrderPriority, "MORE");

            var results = collection.UpdateMany(filter, update);
            logger.LogInformation($"Update completed. IsModifiedCountAvailable: {results.IsModifiedCountAvailable}.\r\n\t {results.MatchedCount} - Matched\r\n\t {results.ModifiedCount} - Modified");

        }
    }
}
