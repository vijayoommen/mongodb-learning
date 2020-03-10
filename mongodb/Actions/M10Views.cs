using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using mongodb.Documents;
using MongoDB.Driver;

namespace mongodb.Actions
{
    class M10Views : IMenuAction
    {
        public int Id => 10;
        public string Description => "Create Views";
        public void Execute(IAppConfigurations configurations, AppMongoClient appMongoClient, ILogger logger)
        {
            //appMongoClient.MongoDatabase.CreateView("RevenueByCountry", "SalesRecords", );
            //var collection = appMongoClient.MongoDatabase.GetCollection<SalesRecord>("SalesRecords");

            //var y = new ProjectionDefinitionBuilder<SalesRecord>().Expression(
            //    (s) => new RevenueByCountry()
            //    {
            //        Country = s.Country, TotalCost = s.TotalCost, TotalProfit = s.TotalProfit,
            //        TotalRevenue = s.TotalRevenue
            //    });

            ////var s = PipelineStageDefinitionBuilder.Group<RevenueByCountry>((SalesRecord s) => new RevenueByCountry()
            ////{
            ////    Country = s.Country,
            ////    TotalCost = s.TotalCost,
            ////    TotalProfit = s.TotalProfit,
            ////    TotalRevenue = s.TotalRevenue
            ////};

            //var grouping = collection
            //    .Aggregate()
            //    .Group(r => new {GroupedCountry = r.Country},
            //        g => new RevenueByCountry()
            //        {
            //            Country = g.Key.ToString(),
            //            TotalProfit = g.Sum(x => x.TotalProfit),
            //            TotalCost = g.Sum(x => x.TotalCost),
            //            TotalRevenue = g.Sum(x => x.TotalRevenue)
            //        }
            //    );
            //var pipelineStageDef = PipelineStageDefinitionBuilder.Group(new ProjectionDefinitionBuilder<SalesRecord>().Expression(r => new { r.Country }));

        }
    }
}
