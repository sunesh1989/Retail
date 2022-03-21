using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RetailManagement.Shared.Product;

namespace RetailManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IBus _messageBus;

        public ProductController(IBus iBus)
        {
            _messageBus = iBus;
        }

        // GET: api/<controller>
        [HttpGet("{topRecordCount}/{page}")]
        public IActionResult Get(int topRecordCount = 100, int page = 1)
        {
            var reponse = _messageBus.Rpc.Request<ProductRequest, ProductResponse>(new ProductRequest
            {
                Records = topRecordCount,
                PageNumber = page
            });
            return Ok(reponse);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var reponse = _messageBus.Rpc.Request<ProductItemRequest, ProductResponse>(new ProductItemRequest
            {
                Parameter = id
            });
            return Ok(reponse);
        }
    }
}
