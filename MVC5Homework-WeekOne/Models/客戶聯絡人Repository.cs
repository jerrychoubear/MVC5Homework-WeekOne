using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MVC5Homework_WeekOne.Models
{
    public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
    {
        public override IQueryable<客戶聯絡人> All()
        {
            return Where(a => a.是否已刪除 == false).Include(a => a.客戶資料).OrderBy(a => a.Id);
        }

        public IQueryable<客戶聯絡人> All(string 查詢條件_名稱, string 查詢條件_職稱)
        {
            return All()
                .Where(a => string.IsNullOrEmpty(查詢條件_名稱) || a.姓名.Contains(查詢條件_名稱))
                .Where(a => string.IsNullOrEmpty(查詢條件_職稱) || a.職稱 == 查詢條件_職稱);
        }

        internal 客戶聯絡人 GetClientContact(int id)
        {
            return All().FirstOrDefault(a => a.Id == id);
        }

        internal bool IsEmailExists(客戶聯絡人 客戶聯絡人)
        {
            return All().Any(a => a.Email == 客戶聯絡人.Email && a.Id != 客戶聯絡人.Id);
        }

        internal IEnumerable<客戶聯絡人> GetDistinctByTitle()
        {
            return All()
                .OrderBy(p => p.職稱)
                .GroupBy(p => p.職稱)
                .Select(p => p.FirstOrDefault());
        }
    }

    public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {

    }
}