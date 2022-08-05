using DCMS.Application.Interfaces.Repositories;
using DCMS.Domain.Entities.Catalog;

namespace DCMS.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IRepositoryAsync<Brand, int> _repository;

        public BrandRepository(IRepositoryAsync<Brand, int> repository)
        {
            _repository = repository;
        }
    }
}