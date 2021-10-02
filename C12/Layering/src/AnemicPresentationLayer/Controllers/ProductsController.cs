using AnemicDomainLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnemicPresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDetails>> Get()
        {
            var products = _productService
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
