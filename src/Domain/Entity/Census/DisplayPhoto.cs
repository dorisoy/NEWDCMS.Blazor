using System;

namespace DCMS.Domain.Census
{
    /// <summary>
    /// 陈列照片
    /// </summary>
    public class DisplayPhoto : AuditableEntity<int>
    {
        public string DisplayPath { get; set; }

        public int TraditionId { get; set; }
        public virtual Tradition Tradition { get; set; }


        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public int VisitStoreId { get; set; }
    
        public DateTime UpdateDate { get; set; }


    }
}
