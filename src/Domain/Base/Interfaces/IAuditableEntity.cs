using System;

namespace DCMS.Domain
{
    public interface IAuditableEntity<TId> : IAuditableEntity, IEntity<TId>
    {
    }

    public interface IAuditableEntity : IEntity
    {
        int StoreId { get; set; }

        ///// <summary>
        ///// 最后创建人
        ///// </summary>
        //string CreatedBy { get; set; }

        ///// <summary>
        ///// 最后创建时间
        ///// </summary>
        //DateTime CreatedOn { get; set; }

        ///// <summary>
        ///// 最后修改人
        ///// </summary>
        //string LastModifiedBy { get; set; }

        ///// <summary>
        ///// 最后修改时间
        ///// </summary>
        //DateTime? LastModifiedOn { get; set; }
    }
}