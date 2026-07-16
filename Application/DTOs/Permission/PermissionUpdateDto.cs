using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Permission
{
    public class PermissionUpdateDto
    {
        public int PermissionId { get; set; }

        [Required]
        public string PermissionName { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}