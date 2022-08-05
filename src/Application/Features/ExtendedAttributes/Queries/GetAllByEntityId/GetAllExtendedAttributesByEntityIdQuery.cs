using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DCMS.Application.Interfaces.Repositories;
using DCMS.Domain;
using DCMS.Shared.Constants.Application;
using DCMS.Shared.Wrapper;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DCMS.Application.Features.ExtendedAttributes.Queries.GetAllByEntityId
{
    public class GetAllExtendedAttributesByEntityIdQuery<TId, TEntityId, TEntity, TExtendedAttribute>
        : IRequest<Result<List<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>>>>
            where TEntity : AuditableEntity<TEntityId>, IEntityWithExtendedAttributes<TExtendedAttribute>, IEntity<TEntityId>
            where TExtendedAttribute : AuditableEntityExtendedAttribute<TId, TEntityId, TEntity>, IEntity<TId>
            where TId : IEquatable<TId>
    {
        public TEntityId EntityId { get; set; }

        public GetAllExtendedAttributesByEntityIdQuery(TEntityId entityId)
        {
            EntityId = entityId;
        }
    }

    internal class GetAllExtendedAttributesByEntityIdQueryHandler<TId, TEntityId, TEntity, TExtendedAttribute>
        : IRequestHandler<GetAllExtendedAttributesByEntityIdQuery<TId, TEntityId, TEntity, TExtendedAttribute>, Result<List<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>>>>
            where TEntity : AuditableEntity<TEntityId>, IEntityWithExtendedAttributes<TExtendedAttribute>, IEntity<TEntityId>
            where TExtendedAttribute : AuditableEntityExtendedAttribute<TId, TEntityId, TEntity>, IEntity<TId>
            where TId : IEquatable<TId>
    {
        private readonly IUnitOfWork<TId> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllExtendedAttributesByEntityIdQueryHandler(IUnitOfWork<TId> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>>>> Handle(GetAllExtendedAttributesByEntityIdQuery<TId, TEntityId, TEntity, TExtendedAttribute> request, CancellationToken cancellationToken)
        {
            Func<Task<List<TExtendedAttribute>>> getAllExtendedAttributesByEntityId = () => _unitOfWork.Repository<TExtendedAttribute>().Entities.Where(x => x.EntityId.Equals(request.EntityId)).ToListAsync(cancellationToken);
            var extendedAttributeList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllEntityExtendedAttributesByEntityIdCacheKey(typeof(TEntity).Name, request.EntityId), getAllExtendedAttributesByEntityId);
            var mappedExtendedAttributes = _mapper.Map<List<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>>>(extendedAttributeList);
            return await Result<List<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>>>.SuccessAsync(mappedExtendedAttributes);
        }
    }
}