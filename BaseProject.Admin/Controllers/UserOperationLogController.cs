using BaseProject.Service;
using BaseProject.Admin.ViewModels.UserOperationLog;
using Core.Base;
using Core.Common.Repository;
using Core.Utility.ValidationError;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BaseProject.Admin.Controllers
{
    public class UserOperationLogController : CoreBaseController
    {

        IUnitOfWork db;
        private IUserOpertationLogService service;
        public UserOperationLogController(IUnitOfWork inDb, IUserOpertationLogService inService)
        {
            db = inDb;
            service = inService;
            service.InitialiseIValidationDictionary(new ModelStateWrapper(this.ModelState));
        }

        // GET: UserOperationLog
        public ActionResult Index(Index searchViewModel)
        {
            service.ProcessIndexViewModel(searchViewModel);
            return View(searchViewModel);
        }

        public ActionResult ExportExcel(Index searchViewModel)
        {
            List<int> ids = searchViewModel.SeletedIdInList.Select(int.Parse).ToList();
            service.ProcessExportViewModel(searchViewModel, x => ids.Contains(x.Id));
            return ExportExcel(searchViewModel.Result);
        }
    }
}