using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services
{
    public interface ICriteriaService
    {
        public Task<List<CriteriaResponse>> GetCriteriaTreeAsync();

        public Task<List<CriteriaResponse>> GetCriteriaFlatAsync();

        public Task CreateAsync(CriteriaRequest request);
        public Task UpdateAsync(int idCriteria, CriteriaRequest request);
        public Task<bool> DeleteAsync(int id);
        public Task<CriteriaResponse?> GetCriteriaByID(int Id);
    }

}
