using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ProductImage
{
    public class ProductImageCreateDto
    {
        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsMain { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}