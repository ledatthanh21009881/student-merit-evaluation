using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;
using PJ_XET_THI_DUA_KHEN_THUONG.Repositories;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services
{
    public class StudentEvaluationService : IStudentEvaluationService
    {
        private readonly IStudentEvaluationRepository _repository;

        public StudentEvaluationService(IStudentEvaluationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EvaluationDto>> GetEvaluationFormAsync(int studentId, int semesterId, int academicYearId)
        {
            return await _repository.GetEvaluationFormAsync(studentId, semesterId, academicYearId);
        }

        public async Task<bool> SaveEvaluationAsync(SaveEvaluationRequestDto request)
        {
            return await _repository.SaveEvaluationAsync(request);
        }

        public async Task<bool> ConfirmEvaluationAsync(int studentId, int semesterId, int academicYearId)
        {
            return await _repository.ConfirmEvaluationAsync(studentId, semesterId, academicYearId);
        }
    }
}
