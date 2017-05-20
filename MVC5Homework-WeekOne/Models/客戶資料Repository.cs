using System;
using System.Collections.Generic;
using System.Linq;

namespace MVC5Homework_WeekOne.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public override IQueryable<客戶資料> All()
        {
            return Where(a => !a.是否已刪除).OrderBy(a => a.Id);
        }

        public IQueryable<客戶資料> All(string ClientName, string ClientClass)
        {
            return All()
                .Where(a => (ClientName ?? string.Empty).Length == 0 || a.客戶名稱.Contains(ClientName))
                .Where(a => (ClientClass ?? string.Empty).Length == 0 || a.客戶分類 == ClientClass);
        }

        public 客戶資料 GetClient(int id)
        {
            return All().FirstOrDefault(a => a.Id == id);
        }

        internal IEnumerable<dynamic> GetExcelData(string ClientName, string ClientClass)
        {
            return All().Select(p => new { p.客戶名稱, p.統一編號, p.電話, p.傳真, p.地址, p.Email, p.客戶分類 }).AsEnumerable();
        }
    }

    public  interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}