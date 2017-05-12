namespace MVC5Homework_WeekOne.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶聯絡人與銀行帳戶數量一覽表MetaData))]
    public partial class 客戶聯絡人與銀行帳戶數量一覽表
    {
    }
    
    public partial class 客戶聯絡人與銀行帳戶數量一覽表MetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        public Nullable<int> 銀行帳戶數量 { get; set; }
        public Nullable<int> 聯絡人數量 { get; set; }
    }
}
