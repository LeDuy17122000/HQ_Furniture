namespace Application.DTOs.RolePermission
{
    public class AssignPermissionDto
    {
        public int RoleId { get; set; }

        public List<int> PermissionIds { get; set; } = new();
    }
}