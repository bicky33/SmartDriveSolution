using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Base;

namespace Service.Master
{
    public class CategoryService : IServiceEntityBase<CategoryResponse>
    {
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public CategoryService(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<CategoryResponse> CreateAsync(CategoryResponse entity)
        {
            var category = entity.Adapt<Category>();
            _repositoryManagerMaster.CategoryRepository.CreateEntity(category);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return category.Adapt<CategoryResponse>();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _repositoryManagerMaster.CategoryRepository.GetEntityById(id, false);
            if (category == null)
            {
                throw new EntityNotFoundException(id, nameof(category));
            }
            _repositoryManagerMaster.CategoryRepository.DeleteEntity(category);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync(bool trackChanges)
        {
            var categories = await _repositoryManagerMaster.CategoryRepository.GetAllEntity(false);
            var categoriesResponse = categories.Adapt<IEnumerable<CategoryResponse>>();
            return categoriesResponse;
        }

        public async Task<CategoryResponse> GetByIdAsync(int id, bool trackChanges)
        {
            var category = await _repositoryManagerMaster.CategoryRepository.GetEntityById(id, false);
            if (category == null)
            {
                throw new EntityNotFoundException(id, nameof(category));
            }
            var categoryResponse = category.Adapt<CategoryResponse>();
            return categoryResponse;
        }

        public async Task UpdateAsync(int id, CategoryResponse entity)
        {
            var category = await _repositoryManagerMaster.CategoryRepository.GetEntityById(id, true);
            if (category == null)
            {
                throw new EntityNotFoundException(id, nameof(entity));
            }
            category.CateId = entity.CateId;
            category.CateName = entity.CateName;

            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }
    }
}