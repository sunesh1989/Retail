using MongoDB.Driver;

namespace RetailManagementService.DataContext
{
    public interface IMongoDBContext
    {
        IMongoCollection<Product> GetCollection<Product>(string name);
        public string ProductCollectionName { get; set; }
        public string ReductionCollectionName { get; set; }
    }
}
