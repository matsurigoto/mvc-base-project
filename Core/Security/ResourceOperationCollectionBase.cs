using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Security;
using MvcSiteMapProvider;

namespace Core.Security
{
    /// <summary>
    /// 代表目前這個站台所有的Route對應的Resource和Operation。
    /// 此實作應該以DI用static的方式注入。
    /// </summary>
    public class ResourceOperationCollectionBase : IResourceOperationCollection
    {
        /// <summary>
        /// 這個站台的Resource和Operation。
        /// </summary>
        private List<ResourceOperation> resourceOperation;

        /// <summary>
        /// 取得或設定這個站台的Resource和Operation。
        /// </summary>
        /// <value>
        /// 這個站台的Resource和Operation。
        /// </value>
        public List<ResourceOperation> ResourceOperation
        {
            get
            {
                if (resourceOperation == null)
                {
                    ResourceOperation = new List<ResourceOperation>();
                }

                // 避免透過取得List清單之後，直接修改內容，造成實際的權限受到修改

                return resourceOperation.Select(x => new ResourceOperation(x)).ToList();
            }

            private set
            {
                resourceOperation = value;
            }
        }

        /// <summary>
        /// 取得或設定這個站台的Resource和Operation清單是否已經產生。
        /// </summary>
        /// <value>
        ///   <c>true</c>這個站台的Resource和Operation清單已經產生。; 要不然, <c>false</c>.
        /// </value>
        public bool IsResourceOperationPopulated { get; set; }

        /// <summary>
        /// 依照對應的Area、Controller和Action，取得對應的ResourceOperation
        /// </summary>
        /// <param name="areaName">Area名稱</param>
        /// <param name="controllerName">Controller名稱</param>
        /// <param name="actionName">Action名稱</param>
        /// <returns>
        /// 這個Route對應的ResourceOperation
        /// </returns>
        public ResourceOperation GetResourceOperation(string areaName, string controllerName, string actionName)
        {
            return ResourceOperation.Where(x => x.AreaName.ToLower() == areaName.ToLower()
                && x.ControllerName.ToLower() == controllerName.ToLower()
                && x.ActionName.ToLower() == actionName.ToLower()).FirstOrDefault();
        }

        /// <summary>
        /// 依照Request context，取得對應的ResourceOperation
        /// </summary>
        /// <param name="context">Request Context</param>
        /// <returns>
        /// 這個Route對應的ResourceOperation
        /// </returns>
        public ResourceOperation GetResourceOperation(System.Web.HttpContextBase context)
        {
            var routeData = context.Request.RequestContext.RouteData;

            var area = routeData.Values.ContainsKey("area") == true ? routeData.GetRequiredString("area") : string.Empty;
            var controller = routeData.GetRequiredString("controller");
            var action = routeData.GetRequiredString("action");

            return GetResourceOperation(area, controller, action);
        }

        /// <summary>
        /// 產生這個站台的Resource和Operation清單
        /// </summary>
        /// <returns>
        /// 站台的Resource和Operation清單
        /// </returns>
        protected List<ResourceOperation> GenResourceOperationCollection()
        {
            var result = new List<ResourceOperation>();

            var root = SiteMaps.Current.RootNode;

            result.Add(new ResourceOperation()
            {
                ActionName = root.Action,
                ControllerName = root.Controller,
                AreaName = root.Area,
                ResourceName = root.Attributes["Resource"].ToString(),
                DisplayNameWithParent = root.Attributes["Resource"].ToString(),
                DisplayName = root.Title,
                Operation = (ResourceOperationEnum)Enum.Parse(typeof(ResourceOperationEnum), root.Attributes["Operation"].ToString())
            });

            foreach (var item in root.Descendants)
            {
                if (item.Attributes.ContainsKey("Resource") && item.Attributes.ContainsKey("Operation"))
                {
                    result.Add(new ResourceOperation()
                    {
                        ActionName = item.Action,
                        ControllerName = item.Controller,
                        AreaName = item.Area,
                        ResourceName = item.Attributes["Resource"].ToString(),
                        DisplayName = item.Title,
                        DisplayNameWithParent = GetResourceDisplayNameWithParent(item),
                        Operation = (ResourceOperationEnum)Enum.Parse(typeof(ResourceOperationEnum), item.Attributes["Operation"].ToString())
                    });
                }
            }

            this.IsResourceOperationPopulated = true;

            return result;
        }

        /// <summary>
        /// 取得Resource的顯示名字 - 會取得父的Title加上目前的Title
        /// </summary>
        /// <param name="sitemapNode">目前的sitemap</param>
        /// <returns>父的Title加上目前的Title</returns>
        private string GetResourceDisplayNameWithParent(ISiteMapNode sitemapNode)
        {
            string result = sitemapNode.Title;

            if (sitemapNode.ParentNode != null)
            {
                result = sitemapNode.ParentNode.Title + "/" + sitemapNode.Title;
            }

            return result;
        }

        /// <summary>
        /// 產生並且設定這個站台的Resource和Operation清單
        /// </summary>
        public void PopulateResourceOperationCollection()
        {
            ResourceOperation = GenResourceOperationCollection();
        }
    }
}
