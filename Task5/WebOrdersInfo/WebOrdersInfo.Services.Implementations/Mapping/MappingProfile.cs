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

            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Manager, ManagerDto>();
            CreateMap<ManagerDto, Manager>();

            CreateMap<Order, OrderWithNamesDto>()
                .ForMember(o => o.ClientName, opt => opt.MapFrom(m => m.Client.Name))
                .ForMember(o => o.ManagerName, opt => opt.MapFrom(m => m.Manager.Name))
                .ForMember(o => o.ProductName, opt => opt.MapFrom(m => m.Product.Name));


        }
    }
}