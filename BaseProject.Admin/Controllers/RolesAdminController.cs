using BaseProject.Admin.Security.AuthorizationManager;
using BaseProject.Admin.Security.ViewModel;
using BaseProject.Admin.ViewModels.Account;
using Core.Base;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Core.Common.Security;
using MoreLinq;
using Core.Utility.Alert;

namespace BaseProject.Admin.Controllers
{
    public class RolesAdminController : CoreBaseController
    {
        public RolesAdminController(IResourceOperationCollection inResourceCollection)
        {
            ResourceCollection = inResourceCollection;
        }

        public IResourceOperationCollection ResourceCollection { get; set; }

        //public RolesAdminController(ApplicationUserManager userManager,
        //    ApplicationRoleManager roleManager)
        //{
        //    UserManager = userManager;
        //    RoleManager = roleManager;
        //}

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Roles/
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        //
        // GET: /Roles/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            // Get the list of Users in this Role
            var users = new List<ApplicationUser>();

            // Get the list of Users in this Role
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            var resourceOperation = ResourceCollection.ResourceOperation.DistinctBy(x => x.ResourceName).ToList();

            ResourceOperationPermission rop = new ResourceOperationPermission();

            var permissions = rop.GetCurrentRolePermission(role.Name, role.Id, resourceOperation);

            var viewModel = new Detail()
            {
                ResourceOperation = permissions.ResourceOperation,
                RoleId = permissions.RoleId,
                RoleName = permissions.RoleName,
                Users = users.ToDictionary(x => x.Email, x => x.Email)
            };

            return View(viewModel);
        }

        //
        // GET: /Roles/Create
        public ActionResult Create()
        {
            var resourceOperation = ResourceCollection.ResourceOperation.DistinctBy(x => x.ResourceName)
                                    .ToList();

            resourceOperation.ForEach(x => x.Operation = ResourceOperationEnum.None);

            var viewModel = new Create()
            {
                ResourceOperation = resourceOperation
            };

            return View(viewModel);
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public async Task<ActionResult> Create(Create roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole(roleViewModel.RoleName);
                var roleresult = await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First());
                    return View(roleViewModel);
                }
                roleViewModel.RoleId = role.Id;

                ResourceOperationPermission rop = new ResourceOperationPermission();

                rop.SaveCurrentRolePermission(roleViewModel);
                return RedirectToAction("Index").WithSuccess("新增群組成功");
            }

            return View(roleViewModel);
        }

        //
        // GET: /Roles/Edit/Admin
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            //RoleViewModel roleModel = new RoleViewModel { Id = role.Id, Name = role.Name };

            var resourceOperation = ResourceCollection.ResourceOperation.DistinctBy(x => x.ResourceName).ToList();

            ResourceOperationPermission rop = new ResourceOperationPermission();

            var viewModel = rop.GetCurrentRolePermission(role.Name, role.Id, resourceOperation);

            return View(viewModel);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Edit viewModel)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(viewModel.RoleId);
                role.Name = viewModel.RoleName;
                await RoleManager.UpdateAsync(role);

                ResourceOperationPermission rop = new ResourceOperationPermission();

                rop.SaveCurrentRolePermission(viewModel);

                return RedirectToAction("Index").WithSuccess("修改群組成功");
            }
            return View();
        }

        //
        // GET: /Roles/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id, string deleteUser)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                IdentityResult result;
                if (deleteUser != null)
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                else
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }

                ResourceOperationPermission rop = new ResourceOperationPermission();
                rop.DeleteRolePermissionTable(role.Id);

                return RedirectToAction("Index").WithSuccess("刪除群組成功");
            }
            return View();
        }
    }
}