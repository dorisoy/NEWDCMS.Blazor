using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DCMS.Application.Interfaces.Repositories;
using DCMS.Domain;
using DCMS.Shared.Wrapper;
using MediatR;

namespace DCMS.Application.Features.ExtendedAttributes.Queries.GetById
{
    public class GetExtendedAttributeByIdQuery<TId, TEntityId, TEntity, TExtendedAttribute>
        : IRequest<Result<GetExtendedAttributeByIdModel<TId, TEntityId>>>
        where TEntity : AuditableEntity<TEntityId>, IEntityWithExtendedAttributes<TExtendedAttribute>, IEntity<TEntityId>
        where TExtendedAttribute : AuditableEntityExtendedAttribute<TId, TEntityId, TEntity>, IEntity<TId>
        where TId : IEquatable<TId>
    {
        public TId Id { get; set; }
    }

    internal class GetExtendedAttributeByIdQueryHandler<TId, TEntityId, TEntity, TExtendedAttribute>
        : IRequestHandler<GetExtendedAttributeByIdQuery<TId, TEntityId, TEntity, TExtendedAttribute>, Result<GetExtendedAttributeByIdModel<TId, TEntityId>>>
            where TEntity : AuditableEntity<TEntityId>, IEntityWithExtendedAttributes<TExtendedAttribute>, IEntity<TEntityId>
            where TExtendedAttribute : AuditableEntityExtendedAttribute<TId, TEntityId, TEntity>, IEntity<TId>
            where TId : IEquatable<TId>
    {
        private readonly IUnitOfWork<TId> _unitOfWork;
        private readonly IMapper _mapper;

        public GetExtendedAttributeByIdQueryHandler(IUnitOfWork<TId> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetExtendedAttributeByIdModel<TId, TEntityId>>> Handle(GetExtendedAttributeByIdQuery<TId, TEntityId, TEntity, TExtendedAttribute> query, CancellationToken cancellationToken)
        {
            var extendedAttribute = await _unitOfWork.Repository<TExtendedAttribute>().GetByIdAsync(query.Id);
            var mappedExtendedAttribute = _mapper.Map<GetExtendedAttributeByIdModel<TId, TEntityId>>(extendedAttribute);
            return await Result<GetExtendedAttributeByIdModel<TId, TEntityId>>.SuccessAsync(mappedExtendedAttribute);
        }
    }
}