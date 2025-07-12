using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using PJ_XET_THI_DUA_KHEN_THUONG.Repositories;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services.Implement
{
    public class CriteriaTypeService : ICriteriaTypeService
    {
        private readonly ICriteriaTypeRepository _criteriaTypeRepository;

        public CriteriaTypeService(ICriteriaTypeRepository criteriaTypeRepository)
        {
            _criteriaTypeRepository = criteriaTypeRepository;
        }

        public async Task<List<CriteriaTypeResponse>> GetAllAsync()
        {
            var criteriaTypes = await _criteriaTypeRepository.GetAllAsync();
            return criteriaTypes.Select(ct => new CriteriaTypeResponse
            {
                CriteriaTypeID = ct.CriteriaTypeID,
                CriteriaTypeName = ct.CriteriaTypeName,
            }).ToList();
        }

        public async Task<CriteriaTypeResponse?> GetByIdAsync(int id)
        {
            var criteriaType = await _criteriaTypeRepository.GetByIdAsync(id);
            if (criteriaType == null)
                throw new Exception("Không tìm thấy loại tiêu chí.");

            return new CriteriaTypeResponse
            {
                CriteriaTypeID = criteriaType.CriteriaTypeID,
                CriteriaTypeName = criteriaType.CriteriaTypeName,
            };
        }

        public async Task CreateAsync(CriteriaTypeRequest request)
        {
            // Kiểm tra tên đã tồn tại
            if (await _criteriaTypeRepository.ExistsByNameAsync(request.CriteriaTypeName))
                throw new Exception("Tên loại tiêu chí đã tồn tại.");

            var criteriaType = new CriteriaType
            {
                CriteriaTypeName = request.CriteriaTypeName
            };

            await _criteriaTypeRepository.AddAsync(criteriaType);
        }

        public async Task UpdateAsync(int id, CriteriaTypeRequest request)
        {
            var criteriaType = await _criteriaTypeRepository.GetByIdAsync(id);
            if (criteriaType == null)
            {

                throw new Exception("Không tìm thấy loại tiêu chí.");
            }

            // Kiểm tra tên đã tồn tại (trừ bản ghi hiện tại)
            if (await _criteriaTypeRepository.ExistsByNameAsync(request.CriteriaTypeName, id))
            {
                throw new Exception("Tên loại tiêu chí đã tồn tại.");
            }
            
            criteriaType.CriteriaTypeName = request.CriteriaTypeName;
            await _criteriaTypeRepository.UpdateAsync(criteriaType);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var criteriaType = await _criteriaTypeRepository.GetByIdAsync(id);
            if (criteriaType == null)
                return false;

            // Kiểm tra có tiêu chí nào sử dụng loại này không
            if (criteriaType.CriteriaList?.Any() == true)
            {
                throw new Exception("Không thể xóa loại tiêu chí này vì đang được sử dụng bởi các tiêu chí khác.");
            }

            await _criteriaTypeRepository.DeleteAsync(criteriaType);
            return true;
        }
    }
}