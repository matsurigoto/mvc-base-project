using System;
using System.Linq;
using System.Security.Claims;
using Core.Common.Security;

namespace Core.Security.AuthorizationManager
{
    /// <summary>
    /// 驗證是否有權限使用某一個ResourceOperation的Manager
    /// </summary>
    public abstract class ResourceOperationAuthorisationManager : ClaimsAuthorizationManager
    {
        /// <summary>
        /// When implemented in a derived class, checks authorization for the subject in the specified context to perform the specified action on the specified resource.
        /// </summary>
        /// <param name="context">The authorization context that contains the subject, resource, and action for which authorization is to be checked.</param>
        /// <returns>
        /// true if the subject is authorized to perform the specified action on the specified resource; otherwise, false.
        /// </returns>
        public override bool CheckAccess(AuthorizationContext context)
        {
            bool result = false;

            // 如果沒有Resource和Operation的情況下，表示可以看
            if (context.Resource.Count == 0 && context.Action.Count == 0)
            {
                return true;
            }

            if (context.Principal.Identity.IsAuthenticated)
            {
                string resourceStr = context.Resource.First().Value;
                string operationStr = context.Action.First().Value;

                ResourceOperationEnum operation = (ResourceOperationEnum)
                                                    Enum.Parse(typeof(ResourceOperationEnum), operationStr);

                ResourceOperation resourceOperation = new ResourceOperation()
                {
                    ResourceName = resourceStr,
                    Operation = operation
                };

                var rolesName = context.Principal.FindAll(x => x.Type == ClaimTypes.Role)
                                .Select(x => x.Value).ToArray();

                result = CheckAccess(context, resourceOperation, rolesName);
            }

            return result;
        }

        /// <summary>
        /// 檢查是否有權限使用某個ResourceOperation
        /// </summary>
        /// <param name="context">目前的AuthorizationContext</param>
        /// <param name="resourceOperation">要驗證的ResourceOperation</param>
        /// <param name="rolesName">目前使用者的Role</param>
        /// <returns>true，表示有權限，不然則是false</returns>
        public abstract bool CheckAccess(AuthorizationContext context,
            ResourceOperation resourceOperation, string[] rolesName);
    }
}
