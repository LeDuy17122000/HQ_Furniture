namespace Application.DTOs.ProductImage
{
    public class ProductImageDto
    {
        public int ImageId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsMain { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;
    }
}