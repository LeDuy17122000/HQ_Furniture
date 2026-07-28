namespace Application.DTOs.Order
{
    public class OrderUpdateDto
    {
        public int OrderId { get; set; }

        public string ReceiverName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string ShippingAddress { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}