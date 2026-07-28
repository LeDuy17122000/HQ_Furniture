using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ProductImage
{
    public class ProductImageUpdateDto
    {
        public int ImageId { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsMain { get; set; }

        public int ProductId { get; set; }
    }
}