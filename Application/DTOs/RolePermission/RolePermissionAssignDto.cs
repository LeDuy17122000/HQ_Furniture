namespace Application.DTOs.RolePermission
{
    public class RolePermissionAssignDto
    {
        public int RoleId { get; set; }

        public List<int> PermissionIds { get; set; } = new();
    }
}