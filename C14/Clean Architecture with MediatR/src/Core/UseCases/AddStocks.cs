using Core.Entities;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.UseCases
{
    public class AddStocks
    {
        public class Command : IRequest<int>
        {
            public int ProductId { get; set; }
            public int Amount { get; set; }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IProductRepository _productRepository;
            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.FindByIdAsync(request.ProductId, cancellationToken);
                product.QuantityInStock += request.Amount;
                await _productRepository.UpdateAsync(product, cancellationToken);
                return product.QuantityInStock;
            }
        }
    }
}
