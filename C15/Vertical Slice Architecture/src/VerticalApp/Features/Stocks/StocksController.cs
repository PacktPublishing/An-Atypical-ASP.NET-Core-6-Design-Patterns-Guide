using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerticalApp.Features.Stocks
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
        public async Task<ActionResult<AddStocks.Result>> AddAsync(
            int productId,
            [FromBody] AddStocks.Command command
        )
        {
            command.ProductId = productId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("remove-stocks")]
        public async Task<ActionResult<RemoveStocks.Result>> RemoveAsync(
            int productId,
            [FromBody] RemoveStocks.Command command
        )
        {
            try
            {
                command.ProductId = productId;
                var result = await _mediator.Send(command);
                return Ok(result);
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
    }
}
