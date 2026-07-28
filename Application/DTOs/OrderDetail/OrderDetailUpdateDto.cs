using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Order
{
    public class OrderDetailUpdateDto
    {
        public int OrderDetailId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}