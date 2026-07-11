namespace Application.DTOs.Product
{
    public class ProductDetailDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string? Material { get; set; }

        public string? Color { get; set; }

        public string? Dimensions { get; set; }

        public string? Warranty { get; set; }

        public string? Thumbnail { get; set; }

        public string? CategoryName { get; set; }
    }
}