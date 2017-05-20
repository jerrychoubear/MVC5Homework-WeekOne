using MVC5Homework_WeekOne.Controllers.ActionFilters;
using MVC5Homework_WeekOne.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace MVC5Homework_WeekOne.Controllers
{
    public class 客戶銀行資訊Controller : Controller
    {
        客戶銀行資訊Repository repo = RepositoryHelper.Get客戶銀行資訊Repository();
        客戶資料Repository repo2 = RepositoryHelper.Get客戶資料Repository();

        // GET: 客戶銀行資訊
        [TimeSpentActionFilter]
        public ActionResult Index(string 查詢條件_名稱)
        {
            var data = repo.All(查詢條件_名稱);
            ViewData.Model = data;
            return View();
        }

        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = repo.GetClientBank(id.Value);
            if (data == null)
            {
                return HttpNotFound();
            }
            ViewData.Model = data;
            return View();
        }

        // GET: 客戶銀行資訊/Create
        public ActionResult Create()
        {
            //ViewBag.客戶Id = new SelectList(db.客戶資料.Where(c => c.是否已刪除 == false), "Id", "客戶名稱");
            客戶銀行資訊 客戶銀行資訊 = new 客戶銀行資訊();
            InitDropDownList(客戶銀行資訊);
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶銀行資訊);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            InitDropDownList(客戶銀行資訊);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = repo.GetClientBank(id.Value);
            if (data == null)
            {
                return HttpNotFound();
            }
            InitDropDownList(data);
            return View(data);
        }

        // POST: 客戶銀行資訊/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                repo.Update(客戶銀行資訊);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            InitDropDownList(客戶銀行資訊);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = repo.GetClientBank(id.Value);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // POST: 客戶銀行資訊/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var data = repo.GetClientBank(id);
            repo.Delete(data);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        private void InitDropDownList(客戶銀行資訊 客戶銀行資訊)
        {
            List<SelectListItem> 客戶清單 = new List<SelectListItem>();
            foreach (客戶資料 客 in repo2.All())
            {
                客戶清單.Add(new SelectListItem { Text = 客.客戶名稱, Value = 客.Id.ToString() });
            }
            客戶銀行資訊.客戶清單 = 客戶清單;
        }
    }
}
