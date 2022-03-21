namespace RetailManagementService.DataContext
{
    public class MongoSettings : IMongoSettings
    {
        public string ProductCollectionName { get; set; }
        public string ReductionCollectionName { get; set; }
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; }
    }
}