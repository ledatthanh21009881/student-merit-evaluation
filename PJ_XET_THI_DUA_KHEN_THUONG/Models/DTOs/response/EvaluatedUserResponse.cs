namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response
{
    public class EvaluatedUserResponse
    {
        public int UserID { get; set; }
        public string FullName { get; set; } = null!;
        public string IdentityCode { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? TrainingType { get; set; }
        public string? EducationLevel { get; set; }
        public string? AcademicYear { get; set; }
        public int? ClassID { get; set; }
        public int? FacultyID { get; set; }
        public int? MajorId { get; set; }
        
        // Thông tin đánh giá
        public List<EvaluationSummaryResponse> Evaluations { get; set; } = new List<EvaluationSummaryResponse>();
        public int TotalEvaluations { get; set; }
        public DateTime? LastEvaluationDate { get; set; }
    }

    public class EvaluationSummaryResponse
    {
        public int EvaluationID { get; set; }
        public string FormName { get; set; } = null!;
        public string Semester { get; set; } = null!;
        public int? TotalScore { get; set; }
        public string? Status { get; set; }
        public string? Classification { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? LockedAt { get; set; }
    }
}