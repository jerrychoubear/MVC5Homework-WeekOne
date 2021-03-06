using System;
using System.Data.Entity;
using System.Linq;

namespace MVC5Homework_WeekOne.Models
{
    public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
    {
        public override IQueryable<客戶銀行資訊> All()
        {
            return Where(a => a.是否已刪除 == false).Include(a => a.客戶資料).OrderBy(a => a.Id);
        }

        internal 客戶銀行資訊 GetClientBank(int id)
        {
            return All().FirstOrDefault(a => a.Id == id);
        }

        internal IQueryable<客戶銀行資訊> All(string 查詢條件_名稱)
        {
            return All()
                .Where(a => string.IsNullOrEmpty(查詢條件_名稱) || a.銀行名稱.Contains(查詢條件_名稱));
        }
    }

    public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
    {

    }
}