using AutoMapper;
using Core.Entities;
using Web.Controllers;

namespace Web.Mappers
{
    public class StocksProfile : Profile
    {
        public StocksProfile()
        {
            CreateMap<Product, StocksController.StockLevel>();
        }
    }
}
