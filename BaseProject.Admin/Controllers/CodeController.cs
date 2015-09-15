using System.Net;
using System.Web.Mvc;
using BaseProject.Admin.ViewModels.Code;
using BaseProject.Service;
using Core.Base;
using Core.Utility.ValidationError;
using Core.Common.Repository;
using Core.Utility.Alert;
using System.Web.Routing;

namespace BaseProject.Admin.Controllers
{
    public class CodeController : CoreBaseController
    {
        IUnitOfWork db;
        private ICodeService service;

        public CodeController(IUnitOfWork inDb, ICodeService inService)
        {
            service = inService;
            service.InitialiseIValidationDictionary(new ModelStateWrapper(this.ModelState));
            db = inDb;
        }

        // GET: Code
        public ActionResult Index(BaseProject.Admin.ViewModels.Code.Index searchViewModel)
        {
            service.ProcessIndexViewModel(searchViewModel, null, c => c.CodeMain);

            return View(searchViewModel);
        }

        // GET: Code/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detail code = service.GetSpecificDetailToViewModel<Detail>(x => x.SN == id, x => x.CodeMain);

            if (code == null)
            {
                return HttpNotFound();
            }
            return View(code);
        }

        // GET: SpringsWaterAnalysis/Create
        [ActionName("Create")]
        public ActionResult CreateGet(Create viewModel)
        {
            return View(viewModel);
        }

        // POST: Code/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Create code)
        {
            if (service.CreateViewModelToDatabase(code))
            {
                return RedirectToIndexWithMainId(code.MainID).WithSuccess("新增成功");
            }

            return View(code);
        }

        // GET: Code/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var code = service.GetSpecificDetailToViewModel<Edit>(x => x.SN == id);

            if (code == null)
            {
                return HttpNotFound();
            }

            return View(code);
        }

        // POST: Code/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Edit code)
        {
            if (service.UpdateViewModelToDatabase(code, (x => x.SN == code.SN)))
            {
                return RedirectToIndexWithMainId(code.MainID).WithSuccess("修改成功");
            }

            return View(code);
        }

        private ActionResult RedirectToIndexWithMainId(string mainId)
        {
            // TODO: 需要重構到Service裡面或者其他地方
            var rvd = new RouteValueDictionary();
            rvd.Add(Core.Utility.CoreExpressionHelper.GetExpressionText<BaseProject.Admin.ViewModels.Code.Index>(x => x.SearchForm.MainID), mainId);

            return RedirectToAction("Index", rvd);
        }

        // GET: Code/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detail code = service.GetSpecificDetailToViewModel<Detail>(x => x.SN == id);
            if (code == null)
            {
                return HttpNotFound();
            }
            return View(code);
        }

        // POST: Code/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.Delete(x => x.SN == id);

            return RedirectToAction("Index");
        }
    }
}
