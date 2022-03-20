namespace RetailManagement.Shared.Product
{
    public class ProductRequest
    {
        public int Records { get; set; }
        public int Limit { get; set; }
        public int StartFrom { get; set; }
        public string Parameter { get; set; }
    }

    public class ProductItemRequest
    {
        public string Parameter { get; set; }
    }
}
