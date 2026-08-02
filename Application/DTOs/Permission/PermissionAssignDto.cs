namespace Application.DTOs.RolePermission
{
    public class PermissionAssignDto
    {
        public int RoleId { get; set; }

        public List<int> PermissionIds { get; set; }
            = new List<int>();
    }
}