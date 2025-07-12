using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using PJ_XET_THI_DUA_KHEN_THUONG.Repositories;
using PJ_XET_THI_DUA_KHEN_THUONG.Repositories.Implement;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services.Implement
{
    public class ActivityCategoryService : IActivityCategoryService
    {
        private readonly IActivityCategoryRepository _repository;

        public ActivityCategoryService(IActivityCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ActivityCategoryResponse>> GetAllAsync()
        {
            var list = await _repository.GetAllAcitivityCategories();
            return list.Select(c => new ActivityCategoryResponse
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName,
                Description = c.Description,
                IsActive = c.IsActive
            });
        }

        public async Task<ActivityCategoryResponse?> GetByIdAsync(int id)
        {
            var c = await _repository.GetByIdAsync(id);
            if (c == null) return null;
            return new ActivityCategoryResponse
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName,
                Description = c.Description,
                IsActive = c.IsActive
            };
        }

        public async Task AddAsync(ActivityCategoryRequest request)
        {
            var entity = new ActivityCategory
            {
                CategoryName = request.CategoryName,
                Description = request.Description,
                IsActive = request.IsActive
            };
            await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, ActivityCategoryRequest request)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            entity.CategoryName = request.CategoryName;
            entity.Description = request.Description;
            entity.IsActive = request.IsActive;

            await _repository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            await _repository.DeleteAsync(entity);
            return true;
        }
    }
}
