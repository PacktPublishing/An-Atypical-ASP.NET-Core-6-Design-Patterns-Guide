using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappers
{
    public class ProductMapper : IMapper<Data.Models.Product, Core.Entities.Product>, IMapper<Core.Entities.Product, Data.Models.Product>
    {
        public Core.Entities.Product Map(Data.Models.Product entity)
        {
            return new Core.Entities.Product
            {
                Id = entity.Id,
                Name = entity.Name,
                QuantityInStock = entity.QuantityInStock
            };
        }

        public Data.Models.Product Map(Core.Entities.Product entity)
        {
            return new Data.Models.Product
            {
                Id = entity.Id,
                Name = entity.Name,
                QuantityInStock = entity.QuantityInStock
            };
        }
    }
}
