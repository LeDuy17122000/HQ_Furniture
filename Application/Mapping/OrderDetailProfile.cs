using Application.DTOs.Order;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping
{
    public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product!.ProductName));

            CreateMap<OrderDetail, OrderDetailUpdateDto>();

            CreateMap<OrderDetailUpdateDto, OrderDetail>();
        }
    }
}