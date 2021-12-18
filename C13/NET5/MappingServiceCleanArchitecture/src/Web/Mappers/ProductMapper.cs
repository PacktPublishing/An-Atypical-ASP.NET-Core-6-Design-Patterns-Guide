using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Controllers;

namespace Web.Mappers
{
    public class ProductMapper : IMapper<Product, ProductsController.ProductDetails>
    {
        public ProductsController.ProductDetails Map(Product entity)
        {
            return new ProductsController.ProductDetails(
                id: entity.Id,
                name: entity.Name,
                quantityInStock: entity.QuantityInStock
            );
        }
    }
}
