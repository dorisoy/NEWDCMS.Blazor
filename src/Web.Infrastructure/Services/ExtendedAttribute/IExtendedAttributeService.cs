using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DCMS.Application.Features.ExtendedAttributes.Commands.AddEdit;
using DCMS.Application.Features.ExtendedAttributes.Queries.Export;
using DCMS.Application.Features.ExtendedAttributes.Queries.GetAll;
using DCMS.Application.Features.ExtendedAttributes.Queries.GetAllByEntityId;
using DCMS.Domain;
using DCMS.Shared.Wrapper;
using DCMS.Shared.Services;

namespace DCMS.Web.Infrastructure.Services.ExtendedAttribute
{
    public interface IExtendedAttributeService<TId, TEntityId, TEntity, TExtendedAttribute>
        where TEntity : AuditableEntity<TEntityId>, IEntityWithExtendedAttributes<TExtendedAttribute>, IEntity<TEntityId>
        where TExtendedAttribute : AuditableEntityExtendedAttribute<TId, TEntityId, TEntity>, IEntity<TId>
        where TId : IEquatable<TId>
    {
        Task<IResult<List<GetAllExtendedAttributesModel<TId, TEntityId>>>> GetAllAsync();

        Task<IResult<List<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>>>> GetAllByEntityIdAsync(TEntityId entityId);

        Task<IResult<TId>> SaveAsync(AddEditExtendedAttributeCommand<TId, TEntityId, TEntity, TExtendedAttribute> request);

        Task<IResult<TId>> DeleteAsync(TId id);

        Task<IResult<string>> ExportToExcelAsync(ExportExtendedAttributesQuery<TId, TEntityId, TEntity, TExtendedAttribute> request);
    }
}