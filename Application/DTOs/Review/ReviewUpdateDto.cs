using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Review
{
    public class ReviewUpdateDto
    {
        public int ReviewId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}