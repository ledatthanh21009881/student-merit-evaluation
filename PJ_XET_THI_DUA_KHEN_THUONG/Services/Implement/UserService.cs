using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using PJ_XET_THI_DUA_KHEN_THUONG.Repositories;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //mapping từ Users sang EvaluatedUserResponse
        private static EvaluatedUserResponse MapToEvaluatedUserResponse(Users user)
        {
            return new EvaluatedUserResponse
            {
                UserID = user.UserID,
                FullName = $"{user.LastName} {user.FirstName}",
                IdentityCode = user.IdentityCode,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                TrainingType = user.TrainingType,
                EducationLevel = user.EducationLevel,
                AcademicYear = user.AcademicYear,
                ClassID = user.ClassID,
                FacultyID = user.FacultyID,
                MajorId = user.MajorId,
                Evaluations = user.Evaluations?.Select(e => new EvaluationSummaryResponse
                {
                    EvaluationID = e.EvaluationsID,
                    FormName = e.CriteriaForm?.FormName ?? "",
                    Semester = e.Semester,
                    TotalScore = e.TotalScore,
                    Status = e.Status,
                    Classification = e.Classification,
                    SubmittedAt = e.SubmittedAt,
                    LockedAt = e.LockedAt
                }).ToList() ?? new List<EvaluationSummaryResponse>(),
                TotalEvaluations = user.Evaluations?.Count ?? 0,
                LastEvaluationDate = user.Evaluations?.Max(e => e.SubmittedAt)
            };
        }

        /**
         * Lấy danh sách sinh viên đã được đánh giá
         * @return Danh sách sinh viên đã được đánh giá
         */
        public async Task<List<EvaluatedUserResponse>> GetEvaluatedStudentsAsync()
        {
            var students = await _userRepository.GetStudentsWithEvaluationsAsync();
            return students.Select(MapToEvaluatedUserResponse).ToList();
        }

        /**
         * Lấy danh sách sinh viên đã được đánh giá với bộ lọc
         * @param semester Học kỳ để lọc
         * @param academicYear Năm học để lọc
         * @param status Trạng thái đánh giá để lọc
         * @return Danh sách sinh viên đã được đánh giá theo bộ lọc
         */
        public async Task<List<EvaluatedUserResponse>> GetEvaluatedStudentsFilterAsync(string? semester = null, int? academicYear = null, string? status = null)
        {
            var students = await _userRepository.GetStudentsWithEvaluationsFilterAsync(semester, academicYear, status);
            return students.Select(MapToEvaluatedUserResponse).ToList();
        }

        /**
         * Lấy chi tiết sinh viên đã được đánh giá
         * @param userId ID của sinh viên
         * @return Chi tiết sinh viên đã được đánh giá
         */
        public async Task<EvaluatedUserResponse?> GetEvaluatedStudentDetailsAsync(int userId)
        {
            var student = await _userRepository.GetStudentWithEvaluationDetailsAsync(userId);
            if (student == null)
                throw new Exception("Không tìm thấy sinh viên hoặc sinh viên chưa có đánh giá.");

            return MapToEvaluatedUserResponse(student);
        }

        public async Task<List<EvaluatedUserResponse>> GetStudentsAsync()
        {
            var students = await _userRepository.GetAllStudentsAsync();
            return students.Select(MapToEvaluatedUserResponse).ToList();
        }
    }
}