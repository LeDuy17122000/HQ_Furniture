using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Category
{
    public class CategoryCreateDto
    {
        [Required]
        public string CategoryName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Image { get; set; }
    }
}