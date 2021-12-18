using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Mappers
{
    public class StockMapper : IMapper<Product, Controllers.StocksController.StockLevel>
    {
        public Controllers.StocksController.StockLevel Map(Product entity)
        {
            return new Controllers.StocksController.StockLevel(entity.QuantityInStock);
        }
    }
}
