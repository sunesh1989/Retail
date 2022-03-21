using EasyNetQ;
using Microsoft.Extensions.Configuration;
using RetailManagement.Shared.Product;
using System;
using System.Linq;
using Xunit;

namespace RetailManagementAPI.UnitTest
{
    public class ProductControllerTest
    {
        private const string RabbitMqConnection = "host=localhost";
        
        [Fact]
        public void GetAllProduct()  
        {
            var messageBus = RabbitHutch.CreateBus(RabbitMqConnection);
            var reponse = messageBus.Rpc.Request<ProductRequest, ProductResponse>(new ProductRequest
            {
                Records = 10,
                PageNumber = 1
            });
            Assert.True(reponse.Products.Count > 0);
        }

        [Fact]
        public void GetOnlyOneProduct()
        {
            var messageBus = RabbitHutch.CreateBus(RabbitMqConnection);
            var reponse = messageBus.Rpc.Request<ProductRequest, ProductResponse>(new ProductRequest
            {
                Records = 1,
                PageNumber = 1
            });
            Assert.True(reponse.Products.Count > 0 && reponse.Products.Count == 1);
        }

        [Fact]
        public void GetSpecificProduct()
        {
            var messageBus = RabbitHutch.CreateBus(RabbitMqConnection);
            var products = messageBus.Rpc.Request<ProductRequest, ProductResponse>(new ProductRequest
            {
                Records = 5
            });
            if (products.Products.Count > 0)
            {
                var firstProductId = products.Products.First().Id;
                var reponse = messageBus.Rpc.Request<ProductItemRequest, ProductResponse>(new ProductItemRequest
                {
                    Parameter = firstProductId
                });
                Assert.True(reponse.Product != null && reponse.Product.Id == firstProductId);
            }
            else
            {
                Assert.False(true);
            }
        }
    }
}
