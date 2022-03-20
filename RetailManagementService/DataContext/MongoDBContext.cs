using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace RetailManagementService.DataContext
{
    class MongoDBContext : IMongoDBContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        public string ProductCollectionName { get; set; }
        public string ReductionCollectionName { get; set; }
        public MongoDBContext(IOptions<MongoSettings> configuration)
        {
            _mongoClient = new MongoClient(configuration.Value.ConnectionString ?? "mongodb://127.0.0.1:27017");
            _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName ?? "retail");
            ProductCollectionName = configuration.Value.ProductCollectionName ?? "Product";
            ReductionCollectionName = configuration.Value.ReductionCollectionName ?? "PriceReduction";
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}
