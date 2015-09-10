using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BaseProject.Model;
using Core.Common.Security;
using Core.Common.Repository;
using MvcSiteMapProvider;

namespace Core.Security.Filter
{
    /// <summary>
    /// 權限驗證的Filter
    /// </summary>
    public class AuthoriseFilter : AuthorizeAttribute
    {
        public IUnitOfWork DB { get; set; }

        /// <summary>
        /// 取得或設定目前這個站台所有的Route對應的Resource和Operation
        /// 此Property將由DI以static參數的方式注入
        /// </summary>
        /// <value>
        /// 目前這個站台所有的Route對應的Resource和Operation
        /// </value>
        public IResourceOperationCollection ResourceOperationCollection { get; set; }

        /// <summary>
        /// 取得或設定處理ResourceOperation驗證相關的物件
        /// </summary>
        /// <value>
        /// 處理ResourceOperation驗證相關
        /// </value>
        public ResourceOperationAuthorisation ResourceOperationAuthorisation { get; set; }

        /// <summary>
        /// When overridden, provides an entry point for custom authorization checks.
        /// </summary>
        /// <param name="httpContext">The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <returns>
        /// true if the user is authorized; otherwise, false.
        /// </returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (this.ResourceOperationCollection.IsResourceOperationPopulated == false)
            {
                this.ResourceOperationCollection.PopulateResourceOperationCollection();
            }

            var result = false;

            var resourceOperation = ResourceOperationCollection.GetResourceOperation(httpContext);

            this.ProcessResourceOperation(resourceOperation, httpContext);

            result = ResourceOperationAuthorisation.CheckAccess(ClaimsPrincipal.Current, resourceOperation);

            return result;
        }


        /// <summary>
        /// Processes HTTP requests that fail authorization.
        /// 當使用者未登入的情況下，會導入設定的登入頁
        /// 當使用者是登入的情況下，但是沒有權限，會顯示403。
        /// </summary>
        /// <param name="filterContext">Encapsulates the information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute" />. The <paramref name="filterContext" /> object contains the controller, HTTP context, request context, action result, and route data.</param>
        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new System.Web.Mvc.HttpStatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        /// Duran 當初alan設計的架構是多一層，避免core被綁死，但起專案沒有比較快，需要起專案會有兩個方法：
        /// 1. 用原架構起，但與資料庫資料庫綁死，那不如寫死在這邊。
        /// 2. 不同的資料庫結構，這段一樣要重寫，加入不同資料庫後，在重新匯入改model，步驟繁瑣
        /// 目前直接綁資料庫與model，之後起下一個專案方便。
        /// TODO: 需要重購:將data access層抽出重寫?會自動產生資料庫架構?(需要在規劃與思考)。

        /// <summary>
        /// 傳入parse出來的ResourceOperation，方便在做處理
        /// 應該是用來記錄操作紀錄用的
        /// </summary>
        /// <param name="resourceOpertation">傳入的ResourceOperation</param>
        /// <param name="httpContext">The HTTP context.</param>
        protected virtual void ProcessResourceOperation(ResourceOperation resourceOpertation, HttpContextBase httpContext)
        {
            var currentIdentity = httpContext.User.Identity as ClaimsIdentity;

            if (currentIdentity.FindFirst(ClaimTypes.NameIdentifier) != null)
            {

                var routeData = httpContext.Request.RequestContext.RouteData;

                var area = routeData.Values.ContainsKey("area") == true ? routeData.GetRequiredString("area") : string.Empty;
                var controller = routeData.GetRequiredString("controller");
                var action = routeData.GetRequiredString("action");

                var operationLog = new UserOperationLog()
                {
                    Area = area,
                    Action = action,
                    Controller = controller,
                    VisitDateTime = DateTime.Now,
                    RawUrl = httpContext.Request.RawUrl,
                    UserId = currentIdentity.FindFirst(ClaimTypes.NameIdentifier).Value,
                    IP = httpContext.Request.ServerVariables["REMOTE_ADDR"]
                };

                if (resourceOpertation != null)
                {
                    operationLog.Operation = resourceOpertation.Operation;
                    operationLog.Resource = resourceOpertation.ResourceName;
                    operationLog.ResourceName = resourceOpertation.DisplayNameWithParent;

                }
                else
                {
                    var siteMap = SiteMaps.Current.RootNode.Descendants.Where(x =>
                        x.Controller == controller && x.Area == area && x.Action == action).FirstOrDefault();

                    var resourceName = string.Empty;

                    if (siteMap != null)
                    {
                        resourceName = siteMap.Title;

                        if (siteMap.ParentNode != null)
                        {
                            resourceName = siteMap.ParentNode.Title + "/" + resourceName;
                        }
                    }

                    operationLog.ResourceName = resourceName;
                }

                DB.Repository<UserOperationLog>().Create(operationLog);
                DB.Save();
            }
        }
    }
}
