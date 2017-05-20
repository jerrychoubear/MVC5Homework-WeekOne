using MVC5Homework_WeekOne.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MVC5Homework_WeekOne.Controllers
{
    public class 客戶資料Controller : Controller
    {
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
        客戶聯絡人與銀行帳戶數量一覽表Repository repo2 = RepositoryHelper.Get客戶聯絡人與銀行帳戶數量一覽表Repository();

        // GET: 客戶資料
        public ActionResult Index(string 查詢條件_名稱, string 查詢條件_分類)
        {            
            var data = repo.All(查詢條件_名稱, 查詢條件_分類);
            ViewData.Model = data;
            ViewBag.客戶分類清單 = Get客戶分類清單();
            return View();
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = repo.GetClient(id.Value);
            if (data == null)
            {
                return HttpNotFound();
            }
            ViewData.Model = data;
            return View();
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            var data = new 客戶資料();
            data.客戶分類清單 = Get客戶分類清單();
            ViewData.Model = data;
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            客戶資料.客戶分類清單 = Get客戶分類清單();
            ViewData.Model = 客戶資料;
            return View();
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = repo.GetClient(id.Value);
            if (data == null)
            {
                return HttpNotFound();
            }
            data.客戶分類清單 = Get客戶分類清單();
            ViewData.Model = data;
            return View();
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                repo.Update(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            客戶資料.客戶分類清單 = Get客戶分類清單();
            ViewData.Model = 客戶資料;
            return View();
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶資料 = repo.GetClient(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            ViewData.Model = 客戶資料;
            return View();
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = repo.GetClient(id);

            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            if (客戶資料.客戶銀行資訊.Where(銀行 => 銀行.客戶Id == id && 銀行.是否已刪除 == false).Any())
            {
                ModelState.AddModelError("客戶Id", "該客戶尚存在有效銀行資訊，不可刪除");
            }
            if (客戶資料.客戶聯絡人.Where(聯絡人 => 聯絡人.客戶Id == id && 聯絡人.是否已刪除 == false).Any())
            {
                ModelState.AddModelError("客戶Id", "該客戶尚存在有效聯絡人資訊，不可刪除");
            }
            if (ModelState.IsValid == false)
            {
                return View(客戶資料);
            }
            
            客戶資料.是否已刪除 = true;
            repo.Delete(客戶資料);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Report()
        {
            var data = repo2.All();
            ViewData.Model = data;
            return View();
        }

        private List<SelectListItem> Get客戶分類清單()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "S級", Value = "S" },
                new SelectListItem { Text = "A級", Value = "A" },
                new SelectListItem { Text = "B級", Value = "B" },
                new SelectListItem { Text = "C級", Value = "C" }
            };
        }
    }
}
