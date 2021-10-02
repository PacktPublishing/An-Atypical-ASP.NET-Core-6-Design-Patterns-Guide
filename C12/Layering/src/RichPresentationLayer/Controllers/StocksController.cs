using RichDomainLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichPresentationLayer.Controllers
{
    [ApiController]
    [Route("products/{productId}/")]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;
        public StocksController(IStockService stockService)
        {
            _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
        }

        [HttpPost("add-stocks")]
        public ActionResult<StockLevel> Add(int productId, [FromBody] AddStocksCommand command)
        {
            var quantityInStock = _stockService.AddStock(productId, command.Amount);
            var stockLevel = new StockLevel(quantityInStock);
            return Ok(stockLevel);
        }

        [HttpPost("remove-stocks")]
        public ActionResult<StockLevel> Remove(int productId, [FromBody] RemoveStocksCommand command)
        {
            try
            {
                var quantityInStock = _stockService.RemoveStock(productId, command.Amount);
                var stockLevel = new StockLevel(quantityInStock);
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
