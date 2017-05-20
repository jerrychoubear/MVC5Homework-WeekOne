using System;
using System.Data.Entity;
using System.Linq;

namespace MVC5Homework_WeekOne.Models
{
    public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
    {
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(a => a.是否已刪除 == false).Include(客 => 客.客戶資料);
        }

        public IQueryable<客戶銀行資訊> All(bool isDeleted)
        {
            if (isDeleted)
            {
                return this.All();
            }
            else
            {
                return base.All().Include(客 => 客.客戶資料);
            }
        }

        internal 客戶銀行資訊 GetClientBank(int value)
        {
            return this.All().FirstOrDefault(a => a.Id == value);
        }
    }

    public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
    {

    }
}