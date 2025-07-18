using System;
using System.Collections.Generic;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response
{
    public class EvaluationResponse
    {
        public int EvaluationsID { get; set; }
        public int UserID { get; set; }
        public int CriteriaFormID { get; set; }
        public string Semester { get; set; }
        public int? TotalScore { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public int? LockedBy { get; set; }
        public DateTime? LockedAt { get; set; }
        public string? Classification { get; set; }
        public List<EvaluationDetailResponse> Details { get; set; }
    }
} 