using RetailManagement.Shared.Product;
using System.Collections.Generic;

namespace RetailManagementService.Services
{
    public interface IProductService
    {
        public List<Product> Get(int records, int pageNumber) ;
        public Product GetProductById(string id);
    }
}
