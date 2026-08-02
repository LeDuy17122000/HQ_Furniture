using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebAPI.Authorization
{
    public class PermissionHandler
        : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService permissionService;

        public PermissionHandler(IPermissionService permissionService)
        {
            this.permissionService = permissionService;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            // Lấy UserId từ JWT
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return;

            int userId = int.Parse(userIdClaim.Value);

            // Kiểm tra quyền
            bool hasPermission = await permissionService
                .HasPermissionAsync(userId, requirement.Permission);

            if (hasPermission)
            {
                context.Succeed(requirement);
            }
        }
    }
}