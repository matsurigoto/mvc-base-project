using BaseProject.Admin.Security.ViewModel;
using BaseProject.Model;
using Core.Common.Repository;
using Core.Common.Security;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Admin.Security.AuthorizationManager
{
    /// <summary>
    /// 驗證是否有權限的Helper Class
    /// </summary>
    public class ResourceOperationPermission
    {
        private IUnitOfWork uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceOperationPermission"/> class.
        /// </summary>
        public ResourceOperationPermission()
            : this(new EFUnitOfWork(new BaseProjectEntities()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceOperationPermission"/> class.
        /// </summary>
        /// <param name="inUow">The in uow.</param>
        public ResourceOperationPermission(IUnitOfWork inUow)
        {
            uow = inUow;
        }

        /// <summary>
        /// 取得RoleId裡面所有的權限
        /// </summary>
        /// <param name="rolIds">要搜索的RoleId</param>
        /// <returns>擁有的權限</returns>
        public List<ResourceOperation> GetResourceOperationForRole(string[] rolIds)
        {
            var result = new List<ResourceOperation>();

            foreach (var item in rolIds)
            {
                result.AddRange(GetResourceOperationForRole(item));
            }

            return result;
        }

        /// <summary>
        /// 取得某一筆RoleId的權限
        /// </summary>
        /// <param name="roleId">要搜索的role Id</param>
        /// <returns>擁有的權限</returns>
        public List<ResourceOperation> GetResourceOperationForRole(string roleId)
        {
            var result = new List<ResourceOperation>();

            foreach (var item in GetRolesPermissionForRole(roleId))
            {
                var temp = new ResourceOperation();
                temp.Operation = item.Operation;
                temp.ResourceName = item.ResourceName;

                result.Add(temp);
            }

            return result;
        }

        /// <summary>
        /// 去DB取得某一筆RoleId的權限
        /// </summary>
        /// <param name="roleId">要搜索的role Id</param>
        /// <returns>DB裡面實際儲存的權限資料</returns>
        private List<RolesOperationPermission> GetRolesPermissionForRole(string roleId)
        {
            return uow.Repository<RolesOperationPermission>().Reads().Where(x => x.RoleId == roleId).ToList();
        }

        /// <summary>
        /// 驗證是否有權限使用某個資源
        /// </summary>
        /// <param name="resource">資源</param>
        /// <param name="roleNames">要驗證的群組名稱</param>
        /// <returns>是否有權限</returns>
        public bool Authorize(ResourceOperation resource, string[] roleNames)
        {
            bool authorized = false;

            var roleIds = GetRoleIdFromRoleNames(roleNames);

            foreach (var item in roleIds)
            {
                authorized = Authorize(resource, item);

                if (authorized == true)
                {
                    break;
                }
            }

            return authorized;
        }

        /// <summary>
        /// 用群組名稱清單，取得群組Id清單
        /// </summary>
        /// <param name="roleNames">群組名稱清單</param>
        /// <returns>符合的群組Id清單</returns>
        public string[] GetRoleIdFromRoleNames(string[] roleNames)
        {
            var roleIds = uow.Repository<AspNetRoles>().Reads().Where(x => roleNames.Contains(x.Name)).Select(x => x.Id).ToArray();
            return roleIds;
        }

        /// <summary>
        /// 用群組名稱，取得群組Id
        /// </summary>
        /// <param name="roleName">群組名稱</param>
        /// <returns>
        /// 符合的群組Id
        /// </returns>
        public string GetRoleIdFromRoleName(string roleName)
        {
            return GetRoleIdFromRoleNames(new string[] { roleName }).FirstOrDefault();
        }

        /// <summary>
        /// 驗證是否有權限使用某個資源
        /// </summary>
        /// <param name="resource">資源</param>
        /// <param name="roleId">要驗證的群組Id</param>
        /// <returns>是否有權限</returns>
        public bool Authorize(ResourceOperation resource, string roleId)
        {
            bool authorized = false;

            authorized = uow.Repository<RolesOperationPermission>().Reads().Where(x => x.RoleId == roleId &&
                            x.ResourceName == resource.ResourceName && (x.Operation & resource.Operation) == resource.Operation).Count() > 0;

            return authorized;
        }

        /// <summary>
        /// 取得某一個群組的搜索網站Resource權限權限對應
        /// </summary>
        /// <param name="roleName">群組名稱</param>
        /// <param name="roleId">群組Id</param>
        /// <param name="resourceOperation">網站全部的資源對應</param>
        /// <returns>修改ViewModel</returns>
        public Edit GetCurrentRolePermission(string roleName, string roleId,
            List<ResourceOperation> resourceOperation)
        {
            var currentRolePermission = GetResourceOperationForRole(roleId);

            foreach (var item in resourceOperation)
            {
                var rolePermission = currentRolePermission.Where(x => x.ResourceName == item.ResourceName).FirstOrDefault();

                if (rolePermission != null)
                {
                    item.Operation = rolePermission.Operation;
                }
                else
                {
                    // 表示沒有權限
                    item.Operation = ResourceOperationEnum.None;
                }
            }

            var viewModel = new Edit()
            {
                ResourceOperation = resourceOperation,
                RoleName = roleName,
                RoleId = roleId
            };

            return viewModel;
        }

        /// <summary>
        /// 新增某一筆的權限到Role Id
        /// </summary>
        /// <param name="roleId">要被新增的Role Id</param>
        /// <param name="resourcOperation">權限內容</param>
        private void AddPemissionToRole(string roleId, ResourceOperation resourcOperation)
        {
            uow.Repository<RolesOperationPermission>().Create(new RolesOperationPermission()
            {
                Operation = resourcOperation.Operation,
                ResourceName = resourcOperation.ResourceName,
                RoleId = roleId
            });
        }

        /// <summary>
        /// 儲存設定的權限
        /// </summary>
        /// <param name="viewModel">新增的ViewModel</param>
        public void SaveCurrentRolePermission(Create viewModel)
        {
            // 因為是新增，因此不會有任何目前已有的權限

            SaveRolePermissionToTable(viewModel.RoleId, viewModel.ResourceOperation,
                new List<RolesOperationPermission>());
        }

        /// <summary>
        /// 更新ViewModel設定的權限到DB
        /// </summary>
        /// <param name="viewModel">設定的權限</param>
        public void SaveCurrentRolePermission(Edit viewModel)
        {
            var currentPermission = GetRolesPermissionForRole(viewModel.RoleId);

            SaveRolePermissionToTable(viewModel.RoleId, viewModel.ResourceOperation, currentPermission);
        }

        /// <summary>
        /// 把權限存到DB
        /// </summary>
        /// <param name="roleId">群組 Id</param>
        /// <param name="allPossiblePedrmission">整個網站的Resource和operation對應</param>
        /// <param name="currentPermission">目前要修改的Resource和Operation</param>
        private void SaveRolePermissionToTable(string roleId, List<ResourceOperation> allPossiblePedrmission,
            List<RolesOperationPermission> currentPermission)
        {
            foreach (var item in allPossiblePedrmission)
            {
                var temp = currentPermission.FirstOrDefault(x => x.ResourceName == item.ResourceName);

                if (temp != null)
                {
                    temp.Operation = item.Operation;
                }
                else
                {
                    AddPemissionToRole(roleId, item);
                }
            }

            uow.Save();
        }

        /// <summary>
        /// 刪除某一個群組所有的權限
        /// </summary>
        /// <param name="roleId">要被刪除的群組Id</param>
        public void DeleteRolePermissionTable(string roleId)
        {
            var toBeDelete = uow.Repository<RolesOperationPermission>().Reads().Where(x => x.RoleId == roleId);

            foreach (var item in toBeDelete)
            {
                uow.Repository<RolesOperationPermission>().Delete(item);
            }

            uow.Save();
        }
    }
}
