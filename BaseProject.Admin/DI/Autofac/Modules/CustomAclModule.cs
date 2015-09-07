using Core.Common.Security;
using Core.Security.Filter;
using MvcSiteMapProvider.Security;
using System.Security.Claims;


namespace BaseProject.Admin.DI.Autofac.Modules
{
    /// <summary>
    /// MvcSiteMap provider security triming的客制
    /// </summary>
    public class CustomAclModule : IAclModule
    {
        /// <summary>
        /// 取得或設定Resource和Operation驗證物件
        /// </summary>
        /// <value>
        /// Resource和Operation驗證物件
        /// </value>
        public ResourceOperationAuthorisation ResourceOperationAuthorisation { get; set; }

        /// <summary>
        /// 取得或設定目前整個站台的Resource和Operation對應
        /// </summary>
        /// <value>
        /// 目前整個站台的Resource和Operation對應
        /// </value>
        public IResourceOperationCollection ResourceOperationCollection { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAclModule"/> class.
        /// </summary>
        /// <param name="inResourceOperationCollection">The in resource operation collection.</param>
        /// <param name="inResourceOperationAuthorisation">The in resource operation authorisation.</param>
        public CustomAclModule(IResourceOperationCollection inResourceOperationCollection,
            ResourceOperationAuthorisation inResourceOperationAuthorisation)
        {
            ResourceOperationCollection = inResourceOperationCollection;
            ResourceOperationAuthorisation = inResourceOperationAuthorisation;
        }

        /// <summary>
        /// Determines whether node is accessible to user.
        /// </summary>
        /// <param name="siteMap">The site map.</param>
        /// <param name="node">The node.</param>
        /// <returns>
        /// <c>true</c> if accessible to user; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.NotImplementedException">沒有實作</exception>
        public bool IsAccessibleToUser(MvcSiteMapProvider.ISiteMap siteMap,
            MvcSiteMapProvider.ISiteMapNode node)
        {
            bool result = true;
            if (ResourceOperationCollection.IsResourceOperationPopulated)
            {
                var resourceOperation = ResourceOperationCollection.GetResourceOperation(node.Area, node.Controller, node.Action);

                result = ResourceOperationAuthorisation.CheckAccess(ClaimsPrincipal.Current, resourceOperation);
            }
            return result;
        }
    }
}
