namespace Application.DTOs.Review
{
    public class ReviewStatisticDto
    {
        public int OneStar { get; set; }

        public int TwoStar { get; set; }

        public int ThreeStar { get; set; }

        public int FourStar { get; set; }

        public int FiveStar { get; set; }

        public double AverageRating { get; set; }

        public int TotalReview { get; set; }
    }
}