using Core.Security.AuthorizationManager;

namespace BaseProject.Admin.Security.AuthorizationManager
{
    /// <summary>
    /// 驗證邏輯的實作
    /// </summary>
    public class BaseProjectResourceOperationAuthorisationManager : ResourceOperationAuthorisationManager
    {
        /// <summary>
        /// Checks the access.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="resourceOperation">The resource operation.</param>
        /// <param name="roles">The roles.</param>
        /// <returns>是否驗證成功</returns>
        public override bool CheckAccess(System.Security.Claims.AuthorizationContext context, Core.Common.Security.ResourceOperation resourceOperation, string[] roles)
        {
            ResourceOperationPermission rop = new ResourceOperationPermission();
            bool result = false;
            result = rop.Authorize(resourceOperation, roles);
            return result;
        }

    }
}
