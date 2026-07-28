namespace Application.DTOs.Post
{
    public class PostDto
    {
        public int PostId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }

        public string? Thumbnail { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}