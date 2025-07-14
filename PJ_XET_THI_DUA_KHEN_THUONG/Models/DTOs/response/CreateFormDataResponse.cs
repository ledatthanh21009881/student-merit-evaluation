namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response
{

    public class CreateFormDataResponse
    {
        public List<CriteriaResponse> AvailableCriteria { get; set; } = new List<CriteriaResponse>();
        public List<string> FormTypes { get; set; } = new List<string>();
        public List<string> Semesters { get; set; } = new List<string>();
        public List<string> RoleTargets { get; set; } = new List<string>();
    }
}
