using AutoMapper;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.DAL.Core.Entities;

namespace WebOrdersInfo.Services.Implementations.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            CreateMap<Client, NameDto>();
            CreateMap<NameDto, Client>();

            CreateMap<Product, NameDto>();
            CreateMap<NameDto, Product>();

            CreateMap<Manager, NameDto>();
            CreateMap<NameDto, Manager>();



        }
    }
}