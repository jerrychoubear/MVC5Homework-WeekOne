using MVC5Homework_WeekOne.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using System;
using MVC5Homework_WeekOne.Controllers.ActionFilters;

namespace MVC5Homework_WeekOne.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
        客戶資料Repository repo2 = RepositoryHelper.Get客戶資料Repository();

        // GET: 客戶聯絡人
        [TimeSpentActionFilter]
        public ActionResult Index(string 查詢條件_名稱, string 查詢條件_職稱)
        {
            var data = repo.All(查詢條件_名稱, 查詢條件_職稱);
            ViewData.Model = data;
            ViewBag.職稱清單 = Get職稱清單();
            return View();
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = repo.GetClientContact(id.Value);
            if (data == null)
            {
                return HttpNotFound();
            }
            ViewData.Model = data;
            return View();
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            //ViewBag.客戶Id = new SelectList(db.客戶資料.Where(c => c.是否已刪除 == false), "Id", "客戶名稱");
            客戶聯絡人 客戶聯絡人 = new 客戶聯絡人();
            InitDropDownList(客戶聯絡人);
            ViewData.Model = 客戶聯絡人;
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶聯絡人);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            InitDropDownList(客戶聯絡人);
            ViewData.Model = 客戶聯絡人;
            return View();
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = repo.GetClientContact(id.Value);
            if (data == null)
            {
                return HttpNotFound();
            }
            InitDropDownList(data);
            ViewData.Model = data;
            return View();
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repo.Update(客戶聯絡人);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            InitDropDownList(客戶聯絡人);
            ViewData.Model = 客戶聯絡人;
            return View();
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult 檢查Email是否重複([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {            
            return Json(!repo.IsEmailExists(客戶聯絡人), JsonRequestBehavior.AllowGet);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = repo.GetClientContact(id.Value);
            if (data == null)
            {
                return HttpNotFound();
            }
            ViewData.Model = data;
            return View();
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var data = repo.GetClientContact(id);
            repo.Delete(data);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        private void InitDropDownList(客戶聯絡人 客戶聯絡人)
        {
            List<SelectListItem> data = new List<SelectListItem>();
            foreach (客戶資料 客 in repo2.All())
            {
                data.Add(new SelectListItem { Text = 客.客戶名稱, Value = 客.Id.ToString() });
            }
            客戶聯絡人.客戶清單 = data;
        }

        private List<SelectListItem> Get職稱清單()
        {
            var data = new List<SelectListItem>();
            foreach (var contact in repo.GetDistinctByTitle())
            {
                data.Add(new SelectListItem { Text = contact.職稱, Value = contact.職稱 });
            }
            return data;
        }
    }
}
