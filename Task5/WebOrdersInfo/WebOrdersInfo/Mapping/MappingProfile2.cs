using AutoMapper;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Models.Filters;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.Models.ViewModels.Account;
using WebOrdersInfo.Models.ViewModels.Clients;
using WebOrdersInfo.Models.ViewModels.Managers;
using WebOrdersInfo.Models.ViewModels.Orders;
using WebOrdersInfo.Models.ViewModels.OrdersFilter;
using WebOrdersInfo.Models.ViewModels.Products;


namespace WebOrdersInfo.Mapping
{
    public class MappingProfile2 : Profile
    {
        public MappingProfile2()
        {
            CreateMap<OrdersFilter, OrdersFilterViewModel>();
            CreateMap<OrdersFilterViewModel, OrdersFilter>();
            //.ForMember(o => o.IsValid, opt => opt.MapFrom(src => true));

            CreateMap<RegisterViewModel, User>()
                .ForMember("UserName", opt => opt.MapFrom(r => r.Email));

            CreateMap<OrderWithNamesDto, OrderViewModel>();

            CreateMap<OrderDto, EditOrderViewModel>();
            CreateMap<EditOrderViewModel, OrderDto>();

            CreateMap<CreateOrderViewModel, OrderDto>();

            CreateMap<ManagerDto, CreateManagerViewModel>();
            CreateMap<CreateManagerViewModel, ManagerDto>();

            CreateMap<ManagerDto, ManagerViewModel>();
            CreateMap<ManagerViewModel, ManagerDto>();

            CreateMap<ClientDto, ClientViewModel>();
            CreateMap<ClientViewModel, ClientDto>();

            CreateMap<ClientDto, CreateClientViewModel>();
            CreateMap<CreateClientViewModel, ClientDto>();

            CreateMap<ProductDto, ProductViewModel>();
            CreateMap<ProductViewModel, ProductDto>();

            CreateMap<ProductDto, CreateProductViewModel>();
            CreateMap<CreateProductViewModel, ProductDto>();
        }
    }
}