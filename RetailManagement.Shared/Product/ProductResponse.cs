using System.Collections.Generic;

namespace RetailManagement.Shared.Product
{
    public class ProductResponse
    {
        public ProductResponse()
        {
            Products = new List<Product>();
        }
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
    }
}
