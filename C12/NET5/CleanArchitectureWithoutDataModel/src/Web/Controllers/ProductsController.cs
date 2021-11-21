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
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDetails>> Get()
        {
            var products = _productRepository
                .All()
                .Select(p => new ProductDetails(
                    id: p.Id,
                    name: p.Name,
                    quantityInStock: p.QuantityInStock
                ));
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
