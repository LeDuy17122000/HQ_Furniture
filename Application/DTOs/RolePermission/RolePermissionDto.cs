namespace Application.DTOs.RolePermission
{
    public class RolePermissionDto
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public int PermissionId { get; set; }

        public string PermissionName { get; set; } = string.Empty;
    }
}