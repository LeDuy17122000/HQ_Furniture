namespace Application.DTOs.Product
{
    public class ProductListDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public string? Thumbnail { get; set; }

        public string? CategoryName { get; set; }
    }
}