using AutoMapper;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Filters;
using WebOrdersInfo.DAL.Core.Entities;
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



        }
    }
}