using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Post
{
    public class PostUpdateDto
    {
        public int PostId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }

        public string? Thumbnail { get; set; }

        public bool Status { get; set; }

        public int UserId { get; set; }
    }
}