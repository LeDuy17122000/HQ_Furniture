namespace Application.DTOs.Review
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;
    }
}