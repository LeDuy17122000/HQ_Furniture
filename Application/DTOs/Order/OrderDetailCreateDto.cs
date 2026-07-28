using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Order
{
    public class OrderDetailCreateDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}