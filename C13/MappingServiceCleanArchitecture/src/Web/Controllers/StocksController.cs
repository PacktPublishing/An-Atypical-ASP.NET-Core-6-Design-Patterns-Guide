using Core;
using Core.Entities;
using Core.Interfaces;
using Core.UseCases;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("products/{productId}/")]
    public class StocksController : ControllerBase
    {
        private readonly IMappingService _mappingService;
        public StocksController(IMappingService mappingService)
        {
            _mappingService = mappingService ?? throw new ArgumentNullException(nameof(mappingService));
        }

        [HttpPost("add-stocks")]
        public ActionResult<StockLevel> Add(
            int productId,
            [FromBody] AddStocksCommand command,
            [FromServices] AddStocks useCase
        )
        {
            var product = useCase.Handle(productId, command.Amount);
            var stockLevel = _mappingService.Map<Product, StockLevel>(product);
            return Ok(stockLevel);
        }

        [HttpPost("remove-stocks")]
        public ActionResult<StockLevel> Remove(
            int productId,
            [FromBody] RemoveStocksCommand command,
            [FromServices] RemoveStocks useCase
        )
        {
            try
            {
                var product = useCase.Handle(productId, command.Amount);
                var stockLevel = _mappingService.Map<Product, StockLevel>(product);
                return Ok(stockLevel);
            }
            catch (NotEnoughStockException ex)
            {
                return Conflict(new
                {
                    ex.Message,
                    ex.AmountToRemove,
                    ex.QuantityInStock
                });
            }
        }

        public class AddStocksCommand
        {
            public int Amount { get; set; }
        }

        public class RemoveStocksCommand
        {
            public int Amount { get; set; }
        }

        public class StockLevel
        {
            public StockLevel(int quantityInStock)
            {
                QuantityInStock = quantityInStock;
            }

            public int QuantityInStock { get; set; }
        }
    }
}
