namespace RetailManagementService.DataContext
{
    public class Mongosettings : IMongosettings
    {
        public string ProductCollectionName { get; set; }
        public string ReductionCollectionName { get; set; }
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; }
    }
}