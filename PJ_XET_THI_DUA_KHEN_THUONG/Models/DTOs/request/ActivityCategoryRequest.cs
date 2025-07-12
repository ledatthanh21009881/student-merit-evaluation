namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request
{
    public class ActivityCategoryRequest
    {
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}