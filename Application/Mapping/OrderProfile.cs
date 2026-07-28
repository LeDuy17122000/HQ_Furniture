using Application.DTOs.Order;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User!.FullName));

            CreateMap<OrderCreateDto, Order>();

            CreateMap<OrderUpdateDto, Order>();

            CreateMap<Order, OrderUpdateDto>();
        }
    }
}