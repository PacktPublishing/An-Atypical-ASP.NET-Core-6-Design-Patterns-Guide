using Core.Repositories;
using MediatR;

namespace Core.UseCases;

public class AddStocks
{
    public class Command : IRequest<Result>
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
    public record class Result(int QuantityInStock);

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly IProductRepository _productRepository;
        public Handler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindByIdAsync(request.ProductId, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException(request.ProductId);
            }
            product.AddStock(request.Amount);
            await _productRepository.UpdateAsync(product, cancellationToken);
            return new Result(product.QuantityInStock);
        }
    }
}
