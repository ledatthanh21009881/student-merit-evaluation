using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("EvaluationNotes")]
    public class EvaluationNotes
    {
        [Key]
        public int NoteId { get; set; }
        public int EvaluationId { get; set; }
        public int CriteriaId { get; set; }
        public string? Note { get; set; }
        public int EvaluatedByRole { get; set; }
        public int EvaluatedBy { get; set; }
        public DateTime EvaluatedAt { get; set; }
    }   
}
