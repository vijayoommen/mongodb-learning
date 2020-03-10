using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace mongodb
{
    public class AppMongoClient
    {
        private readonly IAppConfigurations _configurations;
        private MongoClient _mongoClient { get; set; }
        private IMongoDatabase _mongoDatabase { get; }

        public MongoClient MongoDbClient => _mongoClient;

        public IMongoDatabase MongoDatabase => _mongoDatabase;

        public AppMongoClient(IAppConfigurations configurations)
        {
            _configurations = configurations;
            _mongoClient = new MongoClient(_configurations.MongoDbConnectionString());
            _mongoDatabase = _mongoClient.GetDatabase(_configurations.MongoDbDatabase());
        }
    }
}
