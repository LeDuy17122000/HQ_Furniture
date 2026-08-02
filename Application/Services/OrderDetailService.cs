using Application.DTOs.Order;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrderDetailService(
            IOrderDetailRepository orderDetailRepository,
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            this.orderDetailRepository = orderDetailRepository;
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<List<OrderDetailDto>> GetByOrderAsync(int orderId)
        {
            var details = await orderDetailRepository.GetByOrderAsync(orderId);

            return mapper.Map<List<OrderDetailDto>>(details);
        }

        public async Task<List<OrderDetailDto>> GetByProductAsync(int productId)
        {
            var details = await orderDetailRepository.GetByProductAsync(productId);

            return mapper.Map<List<OrderDetailDto>>(details);
        }

        public async Task<OrderDetailDto?> GetByIdAsync(int id)
        {
            var detail = await orderDetailRepository.GetDetailByIdAsync(id);

            if (detail == null)
                return null;

            return mapper.Map<OrderDetailDto>(detail);
        }

        public async Task UpdateAsync(OrderDetailUpdateDto dto)
        {
            var detail = await orderDetailRepository.GetDetailByIdAsync(dto.OrderDetailId);

            if (detail == null)
                throw new Exception("Order Detail not found.");

            detail.Quantity = dto.Quantity;

            detail.TotalPrice = detail.UnitPrice * dto.Quantity;

            await orderDetailRepository.UpdateAsync(detail);

            await orderDetailRepository.SaveAsync();

            // Cập nhật lại TotalAmount của Order
            var details = await orderDetailRepository.GetByOrderAsync(detail.OrderId);

            var order = await orderRepository.GetByIdAsync(detail.OrderId);

            if (order != null)
            {
                order.TotalAmount = details.Sum(x => x.TotalPrice);

                await orderRepository.UpdateAsync(order);

                await orderRepository.SaveAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var detail = await orderDetailRepository.GetDetailByIdAsync(id);

            if (detail == null)
                throw new Exception("Order Detail not found.");

            int orderId = detail.OrderId;

            await orderDetailRepository.DeleteAsync(detail);

            await orderDetailRepository.SaveAsync();

            var details = await orderDetailRepository.GetByOrderAsync(orderId);

            var order = await orderRepository.GetByIdAsync(orderId);

            if (order != null)
            {
                order.TotalAmount = details.Sum(x => x.TotalPrice);

                await orderRepository.UpdateAsync(order);

                await orderRepository.SaveAsync();
            }
        }
    }
}