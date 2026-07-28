using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Post
{
    public class PostCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }

        public string? Thumbnail { get; set; }

        public bool Status { get; set; } = true;

        [Required]
        public int UserId { get; set; }
    }
}