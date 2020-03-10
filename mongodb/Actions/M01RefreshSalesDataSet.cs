using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Resources;
using System.Text;
using Microsoft.Extensions.Logging;
using mongodb.Documents;
using MongoDB.Driver;

namespace mongodb.Actions
{
    public class M01RefreshSalesDataSet : IMenuAction
    {
        public int Id => 1;
        
        public string Description => "Refresh Sales Dataset";
        public void Execute(IAppConfigurations configurations, AppMongoClient appMongoClient)
        {
            throw new NotImplementedException();
        }

        public void Execute(IAppConfigurations configurations, AppMongoClient appMongoClient, ILogger logger)
        {
            int counter = 0;
            var collection = appMongoClient.MongoDatabase.GetCollection<SalesRecord>("SalesRecords", settings:new MongoCollectionSettings(){AssignIdOnInsert = true});

            collection.DeleteMany(s => true);

            foreach (var salesRecord in ReadFromFile())
            {
                collection.InsertOne(salesRecord);
                logger.LogInformation($"inserted order id {salesRecord.OrderId}. Count {++counter}");
            }
        }

        public IEnumerable<SalesRecord> ReadFromFile()
        {
            var resourceName = "mongodb.DataSets.1000 Sales Records.csv";
            var resourceInfo = this.GetType().Assembly.GetManifestResourceNames().Where(x => x == resourceName);
            if (resourceInfo == null)
                throw new Exception("could not find resource - 1000 Sales records.csv");

            using (var stream = new StreamReader(this.GetType().Assembly.GetManifestResourceStream(resourceName)))
            {
                var firstLine = true;
                do
                {
                    var data = stream.ReadLine().Split(',');
                    if (firstLine)
                    {
                        firstLine = false;
                        continue;
                    }

                    var salesRecord = new SalesRecord()
                    {
                        Region = data[0],
                        Country = data[1],
                        ItemType = data[2],
                        SalesChannel = data[3],
                        OrderPriority = data[4],
                        OrderDate = DateTime.Parse(data[5]),
                        OrderId = int.Parse(data[6]),
                        ShipDate = DateTime.Parse(data[7]),
                        UnitsSold = int.Parse(data[8]),
                        UnitPrice = decimal.Parse(data[9]),
                        UnitCost = decimal.Parse(data[10]),
                        TotalRevenue = decimal.Parse(data[11]),
                        TotalCost = decimal.Parse(data[12]),
                        TotalProfit = decimal.Parse(data[13])
                    };
                    
                    yield return salesRecord;

                } while (!stream.EndOfStream);
            }
        }
    }
}
