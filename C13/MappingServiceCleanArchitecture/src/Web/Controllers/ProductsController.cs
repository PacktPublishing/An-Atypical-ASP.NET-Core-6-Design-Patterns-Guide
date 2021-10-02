using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMappingService _mappingService;

        public ProductsController(IProductRepository productRepository, IMappingService mappingService)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mappingService = mappingService ?? throw new ArgumentNullException(nameof(mappingService));
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDetails>> Get()
        {
            var products = _productRepository
                .All()
                .Select(p => _mappingService.Map<Product, ProductDetails>(p));
            return Ok(products);
        }

        public class ProductDetails
        {
            public ProductDetails(int id, string name, int quantityInStock)
            {
                Id = id;
                Name = name ?? throw new ArgumentNullException(nameof(name));
                QuantityInStock = quantityInStock;
            }

            public int Id { get; }
            public string Name { get; }
            public int QuantityInStock { get; }
        }
    }
}
