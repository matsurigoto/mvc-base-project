using BaseProject.Admin.ViewModels.UserLoginLog;
using BaseProject.Model;
using Core.Base;
using Core.Common.Repository;
using Core.Common.Service;
using Core.Utility.ValidationError;
using System.Web.Mvc;

namespace BaseProject.Admin.Controllers
{
    public class UserLoginLogController : CoreBaseController
    {

        IUnitOfWork db;
        private IService<UserLoginLog> service;

        public UserLoginLogController(IUnitOfWork inDb, IService<UserLoginLog> inService)
        {
            db = inDb;

            service = inService;

            service.InitialiseIValidationDictionary(new ModelStateWrapper(this.ModelState));
        }

        // GET: UserLoginLog
        public ActionResult Index(Index searchViewModel)
        {
            service.ProcessIndexViewModel(searchViewModel);

            return View(searchViewModel);
        }
    }
}