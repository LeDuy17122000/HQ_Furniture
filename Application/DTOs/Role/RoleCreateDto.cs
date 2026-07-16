using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Role
{
    public class RoleCreateDto
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}