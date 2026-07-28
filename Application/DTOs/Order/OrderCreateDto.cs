using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Order
{
    public class OrderCreateDto
    {
        [Required]
        public string ReceiverName { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required]
        public string PaymentMethod { get; set; } = string.Empty;

        public int UserId { get; set; }

        public List<OrderDetailCreateDto> Details { get; set; }
            = new();
    }
}