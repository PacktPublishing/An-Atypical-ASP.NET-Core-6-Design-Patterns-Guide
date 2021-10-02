using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VerticalApp.Data;
using VerticalApp.Models;

namespace VerticalApp.Features.Stocks
{
    public class RemoveStocks
    {
        public class Command : IRequest<Result>
        {
            public int ProductId { get; set; }
            public int Amount { get; set; }
        }

        public class Result
        {
            public int QuantityInStock { get; set; }
        }

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<Product, Result>();
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Amount).GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly ProductContext _db;
            private readonly IMapper _mapper;

            public Handler(ProductContext db, IMapper mapper)
            {
                _db = db ?? throw new ArgumentNullException(nameof(db));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _db.Products.FindAsync(request.ProductId);
                if (request.Amount > product.QuantityInStock)
                {
                    throw new NotEnoughStockException(product.QuantityInStock, request.Amount);
                }
                product.QuantityInStock -= request.Amount;
                await _db.SaveChangesAsync();

                var result = _mapper.Map<Result>(product);
                return result;
            }
        }
    }
}
