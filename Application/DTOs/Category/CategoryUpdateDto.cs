using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Category
{
    public class CategoryUpdateDto
    {
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Image { get; set; }
    }
}