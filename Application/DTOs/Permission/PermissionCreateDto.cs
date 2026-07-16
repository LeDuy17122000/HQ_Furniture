using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Permission
{
    public class PermissionCreateDto
    {
        [Required]
        public string PermissionName { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}