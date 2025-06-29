using System.Diagnostics;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.EntityFrameworkCore;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<ClassLeaders> ClassLeaders { get; set; }

        public DbSet<EvaluationCriteriaMaster> EvaluationCriteriaMaster { get; set; }
        public DbSet<EvaluationCriteriaSets> EvaluationCriteriaSets { get; set; }
        public DbSet<EvaluationCriteriaSetDetails> EvaluationCriteriaSetDetails { get; set; }
        public DbSet<EvaluationDetails> EvaluationDetails { get; set; }
        public DbSet<DisciplineViolations> DisciplineViolations { get; set; }
        public DbSet<EvaluationLogs> EvaluationLogs { get; set; }
        public DbSet<EvaluationPermissionLocks> EvaluationPermissionLocks { get; set; }
        public DbSet<EvaluationSchedules> EvaluationSchedules { get; set; }
        public DbSet<EvaluationRankings> EvaluationRankings { get; set; }
        public DbSet<Activities> Activities { get; set; }
        public DbSet<ActivityRegistrations> ActivityRegistrations { get; set; }
        public DbSet<EvaluationNotes> EvaluationNotes { get; set; }
        public DbSet<Evaluations> Evaluations { get; set; }
        public DbSet<AdminEvaluationDetails> AdminEvaluationDetails { get; set; }
        public DbSet<EvaluationEvidences> EvaluationEvidences { get; set; }

        // Các bảng tham chiếu cho bộ lọc
        public DbSet<AcademicYears> AcademicYears { get; set; }
        public DbSet<Semesters> Semesters { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Classes> Classes { get; set; }

        public DbSet<Faculties> Faculties { get; set; }
     

    }
}
