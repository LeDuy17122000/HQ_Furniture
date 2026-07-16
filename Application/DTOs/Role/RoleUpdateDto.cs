using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Role
{
    public class RoleUpdateDto
    {
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}