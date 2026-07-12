using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.User
{
    public class UserUpdateDto
    {
        public int UserId { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? Avatar { get; set; }

        public int RoleId { get; set; }
    }
}