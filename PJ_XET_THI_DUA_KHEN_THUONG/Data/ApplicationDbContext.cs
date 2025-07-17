using Microsoft.EntityFrameworkCore;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;


namespace PJ_XET_THI_DUA_KHEN_THUONG.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }

        public DbSet<Accounts> Accounts { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<CriteriaType> CriteriaTypes { get; set; }

        public DbSet<Criteria> Criteria { get; set; }
        public DbSet<CriteriaForm> CriteriaForms { get; set; }
        public DbSet<FormTimeline> FormTimelines { get; set; }

        public DbSet<Activites> Activities { get; set; }

        public DbSet<ActivityRegistration> ActivityRegistrations { get; set; }

        public DbSet<ActivityCriteria> ActivityCriteria { get; set; }

        public DbSet<Evaluations> Evaluations { get; set; }
        public DbSet<EvaluationDetails> EvaluationDetails { get; set; }


        // config relationships for criteria
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Criteria>()
                .HasOne(c => c.ParentCriteria) // mỗi tiêu chỉ có thể có một tiêu chỉ cha
                .WithMany(ct => ct.SubCriteria) // mỗi tiêu chỉ cha có thể có nhiều tiêu chỉ con
                .HasForeignKey(c => c.ParentID) // khóa ngoại trỏ đến tiêu chỉ cha
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình liên kết với CriteriaType
            modelBuilder.Entity<Criteria>()
                .HasOne(c => c.CriteriaType)
                .WithMany(ct => ct.CriteriaList)
                .HasForeignKey(c => c.CriteriaTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình mối quan hệ N-N Criteria với CriteriaForm
            modelBuilder.Entity<CriteriaForm>()
                .HasMany(cf => cf.Criterias)
                .WithMany(c => c.CriteriaForms)
                .UsingEntity<Dictionary<string, object>>(

                  "Form_Criterias", // Tên bảng liên kết

                    left => left
                        .HasOne<Criteria>()
                        .WithMany()
                        .HasForeignKey("CriteriaID")
                        .OnDelete(DeleteBehavior.Cascade), // Xóa liên kết khi Criteria bị xóa

                    right => right
                        .HasOne<CriteriaForm>()
                        .WithMany()
                        .HasForeignKey("CriteriaFormID")
                        .OnDelete(DeleteBehavior.Cascade), // Xóa liên kết khi CriteriaForm bị xóa
                    
                    join =>
                    {
                        join.HasKey("CriteriaFormID", "CriteriaID"); // Khóa chính của bảng liên kết
                        join.ToTable("Form_Criterias"); // Tên bảng liên kết
                    }
                );

            // Cấu hình mqh 1-n CriteriaForm với FormTimeline
            modelBuilder.Entity<FormTimeline>()
                .HasOne(ft => ft.CriteriaForm) // 1 form tiêu chí có thể có nhiều mốc thời gian
                .WithMany(cf => cf.FormTimelines) // mỗi mốc thời gian thuộc về một form tiêu chí
                .HasForeignKey(ft => ft.CriteriaFormID)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình khóa chính kép cho ActivityRegistration
            modelBuilder.Entity<ActivityRegistration>()
                .HasKey(ar => new { ar.ActivityId, ar.UserID });

            // Cấu hình liên kết với Activity
            modelBuilder.Entity<ActivityRegistration>()
                .HasOne(ar => ar.Activities)
                .WithMany(a => a.ActivityRegistrations)
                .HasForeignKey(ar => ar.ActivityId);

            // Cấu hình liên kết với User
            modelBuilder.Entity<ActivityRegistration>()
                .HasOne(ar => ar.User)
                .WithMany(u => u.ActivityRegistrations)
                .HasForeignKey(ar => ar.UserID);

            // Cấu hình liên kết nhiều nhiều giữa Activities và Criteria thông qua ActivityCriteria
            modelBuilder.Entity<ActivityCriteria>()
                .HasKey(ac => new { ac.ActivityId, ac.CriteriaID });

            modelBuilder.Entity<ActivityCriteria>()
                .HasOne(ac => ac.Activities)
                .WithMany(a => a.ActivityCriterias)
                .HasForeignKey(ac => ac.ActivityId);

            modelBuilder.Entity<ActivityCriteria>()
                .HasOne(ac => ac.Criteria)
                .WithMany(c => c.ActivityCriterias)
                .HasForeignKey(ac => ac.CriteriaID);

            // Cấu hình liên kết nhiều nhiều giữa Evaluations với EvaluationDetails

            modelBuilder.Entity<EvaluationDetails>()
                .HasOne(ed => ed.Evaluation)
                .WithMany(e => e.Details)
                .HasForeignKey(ed => ed.EvaluationID);

            // User - Evaluations relationship (1-N)
            modelBuilder.Entity<Evaluations>()
                .HasOne(e => e.User)
                .WithMany(u => u.Evaluations)
                .HasForeignKey(e => e.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            // CriteriaForm - Evaluations relationship (1-N)
            modelBuilder.Entity<Evaluations>()
                .HasOne(e => e.CriteriaForm)
                .WithMany()
                .HasForeignKey(e => e.CriteriaFormID)
                .OnDelete(DeleteBehavior.Restrict);

            // Criteria - EvaluationDetails relationship (1-N)
            modelBuilder.Entity<EvaluationDetails>()
                .HasOne(ed => ed.Criteria)
                .WithMany()
                .HasForeignKey(ed => ed.CriteriaID)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
