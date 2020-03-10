using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Extensions.Logging;
using mongodb.Documents;
using MongoDB.Driver;

namespace mongodb.Actions
{
    public class M02ReadAllSalesData : IMenuAction
    {
        public int Id => 2;
        public string Description => "Read all sales data";
        public void Execute(IAppConfigurations configurations, AppMongoClient appMongoClient, ILogger logger)
        {

            Console.WriteLine($"Region");
            var region = Console.ReadLine();
            Console.WriteLine("Country");
            var country = Console.ReadLine();

            var collection = appMongoClient.MongoDatabase.GetCollection<SalesRecord>("SalesRecords");

            //basic find
            Find(logger, collection, region, country);

            //using filters
            FindUsingFilters(logger, region, country, collection);

            //using contains
            FindWildCard(logger, region, country, collection);
        }

        private void FindWildCard(ILogger logger, string region, string country, IMongoCollection<SalesRecord> collection)
        {
            logger.LogInformation("--------- using wildcard ---------------");

            var filter = Builders<SalesRecord>.Filter.Where(x => x.Region == region && x.Country.Contains(country));
            var docs = collection.Find(filter).ToCursor();

            docs.MoveNext();

            logger.LogInformation($"Found {docs.Current.Count()} documents");

            foreach (var result in docs.Current.AsEnumerable())
            {
                logger.LogInformation($"{result.Region,10} {result.Country,30}");
            }
        }

        private static void FindUsingFilters(ILogger logger, string region, string country, IMongoCollection<SalesRecord> collection)
        {
            logger.LogInformation("--------- using filters ---------------");
            var filter = Builders<SalesRecord>.Filter;

            var regionFilter = filter.Eq(field => field.Region, region);
            var countryFilter = filter.Eq(field => field.Country, country);
            var docs = collection.Find(regionFilter & countryFilter).ToCursor();

            docs.MoveNext();

            logger.LogInformation($"Found {docs.Current.Count()} documents");

            foreach (var result in docs.Current.AsEnumerable())
            {
                logger.LogInformation($"{result.Region,10} {result.Country,30}");
            }
        }

        private static void Find(ILogger logger, IMongoCollection<SalesRecord> collection, string region, string country)
        {
            var results = collection.Find(x => x.Region == region && x.Country == country);

            logger.LogInformation($"Found {results.CountDocuments()} documents");

            foreach (var result in results.ToList())
            {
                logger.LogInformation($"{result.Region,10} {result.Country,30}");
            }
        }
    }
}
