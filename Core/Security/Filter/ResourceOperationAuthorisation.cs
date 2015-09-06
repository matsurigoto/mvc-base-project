using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Security;

namespace Core.Security.Filter
{
    /// <summary>
    /// 處理ResourceOperation驗證相關
    /// </summary>
    public class ResourceOperationAuthorisation
    {
        /// <summary>
        /// Default action claim type.
        /// </summary>
        public const string ActionType = "http://application/claims/authorization/action";

        /// <summary>
        /// Default resource claim type
        /// </summary>
        public const string ResourceType = "http://application/claims/authorization/resource";

        /// <summary>
        /// 取得或設定要使用的ClaimsAuthorizationManager
        /// </summary>
        /// <value>
        /// 要使用的ClaimsAuthorizationManager
        /// </value>
        public ClaimsAuthorizationManager CustomAuthorizationManager { get; set; }

        private Lazy<ClaimsAuthorizationManager> _claimsAuthorizationManager = new Lazy<ClaimsAuthorizationManager>(()
            => new IdentityConfiguration().ClaimsAuthorizationManager);

        /// <summary>
        /// 取得註冊的ClaimsAuthorizationManager
        /// </summary>
        public ClaimsAuthorizationManager AuthorizationManager
        {
            get
            {
                if (CustomAuthorizationManager == null)
                {
                    CustomAuthorizationManager = _claimsAuthorizationManager.Value;
                }

                return CustomAuthorizationManager;
            }
        }

        /// <summary>
        /// 組合出AuthorizationContext交給ClaimsAuthorizationManager去做是否有權限的判斷
        /// </summary>
        /// <param name="principal">目前的Principal</param>
        /// <param name="resourceOperation">要檢查的ResourceOperation</param>
        /// <returns>true，表示有權限，要不然就是fals</returns>
        public bool CheckAccess(ClaimsPrincipal principal, ResourceOperation resourceOperation)
        {
            var authorisationContext = CreateAuthorizationContext(principal, resourceOperation);

            return AuthorizationManager.CheckAccess(authorisationContext);
        }

        /// <summary>
        /// 依照內容建立出AuthorizationContext
        /// </summary>
        /// <param name="principal">目前的Principal</param>
        /// <param name="resourceOperation">目前要驗證的ResourceOperation</param>
        /// <returns>AuthorizationContext</returns>
        public AuthorizationContext CreateAuthorizationContext(ClaimsPrincipal principal, ResourceOperation resourceOperation)
        {
            var actionClaims = new Collection<Claim>();
            
            var resourceClaims = new Collection<Claim>();

            if (resourceOperation != null)
            {
                actionClaims.Add(new Claim(ActionType, resourceOperation.Operation.ToString()));

                resourceClaims.Add(new Claim(ResourceType, resourceOperation.ResourceName));
            }

            return new AuthorizationContext(
                principal,
                resourceClaims,
                actionClaims);
        }
    }
}
