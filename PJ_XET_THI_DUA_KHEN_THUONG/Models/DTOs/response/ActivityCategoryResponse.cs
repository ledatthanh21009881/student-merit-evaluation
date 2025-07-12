namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response
{
    public class ActivityCategoryResponse
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
