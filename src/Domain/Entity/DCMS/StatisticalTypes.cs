using System;
namespace DCMS.Domain.Main
{

    /// <summary>
    /// 用于表示统计类别
    /// </summary>
    public class StatisticalTypes : AuditableEntity<int>
    {

        /// <summary>
        /// 键名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

    }
}
