using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Review
{
    public class ReviewCreateDto
    {
        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }
    }
}