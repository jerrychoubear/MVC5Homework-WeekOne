using System.Linq;

namespace MVC5Homework_WeekOne.Models
{
    public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public override IQueryable<客戶資料> All()
        {           
            return base.All().Where(a => !a.是否已刪除);
        }

        public IQueryable<客戶資料> All(bool isDeleted)
        {          
            if (isDeleted)
            {
                return this.All();
            }
            else
            {
                return base.All();
            }
        }

        public 客戶資料 GetClient(int id)
        {
            return this.All().FirstOrDefault(a => a.Id == id);
        }
    }

    public  interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}