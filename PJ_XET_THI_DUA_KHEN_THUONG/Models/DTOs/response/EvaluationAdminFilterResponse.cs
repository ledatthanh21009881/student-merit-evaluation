namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response
{
    public class EvaluationAdminFilterResponse
    {
        public int UserID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int CriteriaFormID { get; set; }
        public string FormName { get; set; }
        public int AcademicYearStart { get; set; }
        public string Semester { get; set; }
        public int EvaluationID { get; set; }
        public string Status { get; set; }
        public string Classification { get; set; }
    }
}