using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RetailManagement.Shared.Product;

namespace RetailManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var messageBus = RabbitHutch.CreateBus(this._configuration.GetConnectionString("RabbtMq"));
            var reponse = messageBus.Rpc.Request<ProductRequest, ProductResponse>(new ProductRequest
            {
                Records = 5
            });
            return Ok(reponse);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var messageBus = RabbitHutch.CreateBus(this._configuration.GetConnectionString("RabbtMq"));
            var reponse = messageBus.Rpc.Request<ProductRequest, ProductResponse>(new ProductRequest
            {
                Records = 5,
                Parameter = id
            });
            return Ok(reponse);
        }
    }
}
