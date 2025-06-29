using System.ComponentModel.DataAnnotations;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    public class ClassLeaders
    {
        [Key]
        public int ClassLeaderId { get; set; }

        public int StudentId { get; set; }

        public DateTime AppointedDate { get; set; }

        public DateTime? RemovedDate { get; set; }

        public string? Note { get; set; }
    }
}
