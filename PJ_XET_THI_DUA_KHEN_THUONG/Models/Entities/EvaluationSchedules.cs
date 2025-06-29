using System.ComponentModel.DataAnnotations;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    public class EvaluationSchedules
    {
        [Key]
        public int ScheduleId { get; set; }
        public int CriteriaSetId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int RoleId { get; set; }
    }
}
