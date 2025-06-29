using System.ComponentModel.DataAnnotations;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    public class EvaluationPermissionLocks
    {
        [Key]
        public int LockId { get; set; }
        public int StudentId { get; set; }
        public int CriteriaSetId { get; set; }
        public bool IsLocked { get; set; }
        public int LockedByRole { get; set; }
    }
}
