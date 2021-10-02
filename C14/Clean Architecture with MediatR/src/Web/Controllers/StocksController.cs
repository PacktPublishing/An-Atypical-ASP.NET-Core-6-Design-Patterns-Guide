using Core;
using Core.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("products/{productId}/")]
    public class StocksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StocksController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("add-stocks")]
        public async Task<ActionResult<StockLevel>> AddAsync(
            int productId,
            [FromBody] AddStocks.Command command,
            CancellationToken cancellationToken
        )
        {
            command.ProductId = productId;
            var quantityInStock = await _mediator.Send(command, cancellationToken);
            var stockLevel = new StockLevel(quantityInStock);
            return Ok(stockLevel);
        }

        [HttpPost("remove-stocks")]
        public async Task<ActionResult<StockLevel>> RemoveAsync(
            int productId,
            [FromBody] RemoveStocks.Command command,
            CancellationToken cancellationToken
        )
        {
            try
            {
                command.ProductId = productId;
                var quantityInStock = await _mediator.Send(command, cancellationToken);
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

        public record StockLevel(int QuantityInStock);
    }
}
