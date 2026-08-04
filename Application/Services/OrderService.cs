using Application.DTOs.Order;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            var orders = await orderRepository.GetAllWithUserAsync();

            return mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await orderRepository.GetDetailByIdAsync(id);

            if (order == null)
                return null;

            return mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderDto>> GetByUserAsync(int userId)
        {
            var orders = await orderRepository.GetByUserAsync(userId);

            return mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<List<OrderDto>> GetByStatusAsync(string status)
        {
            var orders = await orderRepository.GetByStatusAsync(status);

            return mapper.Map<List<OrderDto>>(orders);
        }

        public async Task AddAsync(OrderCreateDto dto)
        {
            var order = new Order
            {
                ReceiverName = dto.ReceiverName,
                Phone = dto.Phone,
                ShippingAddress = dto.ShippingAddress,
                PaymentMethod = dto.PaymentMethod,
                UserId = dto.UserId,
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalAmount = 0
            };

            await orderRepository.AddAsync(order);
            await orderRepository.SaveAsync();

            decimal total = 0;

            foreach (var item in dto.Details)
            {
                var product = await productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                    throw new Exception($"Product {item.ProductId} not found.");

                var detail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = product.Price * item.Quantity
                };

                total += detail.TotalPrice;

                await orderDetailRepository.AddAsync(detail);
            }

            await orderDetailRepository.SaveAsync();

            order.TotalAmount = total;

            await orderRepository.UpdateAsync(order);

            await orderRepository.SaveAsync();
        }

        public async Task UpdateAsync(OrderUpdateDto dto)
        {
            var order = await orderRepository.GetByIdAsync(dto.OrderId);

            if (order == null)
                throw new Exception("Order not found.");

            order.ReceiverName = dto.ReceiverName;
            order.Phone = dto.Phone;
            order.ShippingAddress = dto.ShippingAddress;
            order.PaymentMethod = dto.PaymentMethod;
            order.Status = dto.Status;

            await orderRepository.UpdateAsync(order);

            await orderRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new Exception("Order not found.");

            await orderRepository.DeleteAsync(order);

            await orderRepository.SaveAsync();
        }
        public async Task ConfirmAsync(int orderId)
        {
            await orderRepository.ConfirmAsync(orderId);
        }

        public async Task ShippingAsync(int orderId)
        {
            await orderRepository.ShippingAsync(orderId);
        }

        public async Task CompleteAsync(int orderId)
        {
            await orderRepository.CompleteAsync(orderId);
        }

        public async Task CancelAsync(int orderId)
        {
            await orderRepository.CancelAsync(orderId);
        }
    }
}