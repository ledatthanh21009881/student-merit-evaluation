using System.ComponentModel.DataAnnotations;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request
{
    public class CriteriaTypeRequest
    {
        [Required(ErrorMessage = "Tên loại tiêu chí là bắt buộc")]
        [MaxLength(255, ErrorMessage = "Tên loại tiêu chí không được vượt quá 255 ký tự")]
        public string CriteriaTypeName { get; set; } = null!;
    }
}