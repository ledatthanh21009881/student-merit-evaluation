using System.ComponentModel.DataAnnotations;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    public class EvaluationAdminConfirms
    {
        [Key]
        public int ConfirmId { get; set; }
        public int StudentId { get; set; }
        public int SemesterId { get; set; }
        public int TotalScore { get; set; }
        public int Deduction { get; set; }
        public string DeductionNote { get; set; } = "";
        public int FinalTotal => TotalScore - Deduction;
        public string Classification { get; set; } = ""; // Xuất sắc, Giỏi,...
    }

}
