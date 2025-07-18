using System.Collections.Generic;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request
{
    public class EvaluationRequest
    {
        public int UserID { get; set; }
        public int CriteriaFormID { get; set; }
        public string Semester { get; set; }
        public List<EvaluationDetailRequest> Details { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }
        public string? Classification { get; set; }
    }
} 