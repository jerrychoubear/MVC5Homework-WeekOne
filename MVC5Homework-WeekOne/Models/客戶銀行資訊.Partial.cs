namespace MVC5Homework_WeekOne.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    [MetadataType(typeof(客戶銀行資訊MetaData))]
    public partial class 客戶銀行資訊
    {
        public List<SelectListItem> 客戶清單 { get; set; }
    }
    
    public partial class 客戶銀行資訊MetaData
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "客戶")]
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 銀行名稱 { get; set; }

        [Range(0, 999)]
        [Required]
        public int 銀行代碼 { get; set; }

        [Range(0, 999)]
        public Nullable<int> 分行代碼 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 帳戶名稱 { get; set; }
        
        [StringLength(20, ErrorMessage="欄位長度不得大於 20 個字元")]
        [Required]
        public string 帳戶號碼 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
