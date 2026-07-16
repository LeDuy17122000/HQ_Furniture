namespace Application.DTOs.Permission
{
    public class PermissionDto
    {
        public int PermissionId { get; set; }

        public string PermissionName { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}