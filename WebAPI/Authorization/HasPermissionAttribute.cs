using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Authorization
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission)
        {
            Policy = permission;
        }
    }
}