using AutoMapper;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Models.Filters;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.Models.ViewModels.Account;
using WebOrdersInfo.Models.ViewModels.Orders;
using WebOrdersInfo.Models.ViewModels.OrdersFilter;


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
        }
    }
}