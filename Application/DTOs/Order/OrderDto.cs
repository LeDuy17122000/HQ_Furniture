namespace Application.DTOs.Order
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public string ReceiverName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string ShippingAddress { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}