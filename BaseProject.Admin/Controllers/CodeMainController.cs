using System.Net;
using System.Web.Mvc;
using BaseProject.Model;
using Core.Utility.ValidationError;
using Core.Utility.Alert;
using Core.Base;
using BaseProject.Admin.Service;
using BaseProject.Admin.ViewModels.CodeMain;

namespace BaseProject.Admin.Controllers
{
    public class CodeMainController : CoreBaseController
    {
        private ICodeMainService service;

        public CodeMainController(ICodeMainService inService)
        {
            service = inService;
            service.InitialiseIValidationDictionary(new ModelStateWrapper(this.ModelState));
        }

        // GET: CodeMain
        public ActionResult Index(BaseProject.Admin.ViewModels.CodeMain.Index  searchViewModel)
        {
            service.ProcessIndexViewModel(searchViewModel);
            return View(searchViewModel).;
        }

        // GET: CodeMain/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detail codeMain = service.GetSpecificDetailToViewModel<Detail>(x => x.ID == id);
            if (codeMain == null)
            {
                return HttpNotFound();
            }
            return View(codeMain);
        }

        // GET: CodeMain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CodeMain/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Create codeMain)
        {
            if (service.CreateViewModelToDatabase(codeMain))
            {
                return RedirectToAction("Index").WithSuccess("新增成功");
            }

            return View(codeMain);
        }

        // GET: CodeMain/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var codeMain = service.GetSpecificDetailToViewModel<Edit>(x => x.ID == id);
            if (codeMain == null)
            {
                return HttpNotFound();
            }
            return View(codeMain);
        }

        // POST: CodeMain/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Edit codeMain)
        {
            if (service.UpdateViewModelToDatabase(codeMain,x => x.ID == codeMain.ID))
            {
                return RedirectToAction("Index").WithSuccess("修改成功");
            }
            return View(codeMain);
        }

        // GET: CodeMain/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detail codeMain = service.GetSpecificDetailToViewModel<Detail>(x => x.ID == id);
            if (codeMain == null)
            {
                return HttpNotFound();
            }
            return View(codeMain);
        }

        // POST: CodeMain/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            service.DeleteWithManyToMany<Code>(x => x.ID == id, x => x.Code);
            return RedirectToAction("Index").WithSuccess("刪除成功");
        }
    }
}
