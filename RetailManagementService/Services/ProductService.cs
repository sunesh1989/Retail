using RetailManagementService.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using RetailManagement.Shared.Product;
using RetailManagementService.DataContext.Modal;
using AutoMapper;

namespace RetailManagementService.Services
{
    public class ProductService : IProductService
    {
        private IMongoDBContext _context;
        private readonly IMapper _mapper;
        public ProductService(IMongoDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        public List<Product> Get(int records,int pageNumber)
        {
            var skipCount = (pageNumber <= 0) ? 0 : ((pageNumber-1) * records);
            return _mapper.Map<List<Product>>(
                _context.GetCollection<RetailProduct>(_context.ProductCollectionName)
                .Find(_ => true)
                .Limit(records)
                .Skip(skipCount).ToList());
        }

        /// <summary>
        /// Get product by product id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductById(string id)
        {
            var result = _context.GetCollection<RetailProduct>(_context.ProductCollectionName).Find(m => m.Id == id).FirstOrDefault();
            if (result != null)
            {
                result.Price -= GetReduction();
                return _mapper.Map<Product>(result);
            }
            return null;
        }

        /// <summary>
        /// Get reduction amount based on the week day
        /// </summary>
        /// <returns></returns>
        private double GetReduction()
        {
            var reductionMaster = _context.GetCollection<PriceReduction>(_context.ReductionCollectionName)
                .Find(m => m.PriceReductionName == "Default").FirstOrDefault();
            var priceReduction = reductionMaster.PriceReductionDayOfWeek
                .Where(m => m.DayOfWeek == (int)DateTime.Now.DayOfWeek)
                .FirstOrDefault()?.PriceReduction ?? 0;
            return priceReduction;
        }
    }
}
