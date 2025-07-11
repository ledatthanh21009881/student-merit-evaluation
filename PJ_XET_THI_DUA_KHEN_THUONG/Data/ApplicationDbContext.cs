using System.Diagnostics;
using DocumentFormat.OpenXml.Bibliography;
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

        // config relationships for criteria
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Criteria>()
                .HasOne(c => c.ParentCriteria) // mỗi tiêu chỉ có thể có một tiêu chỉ cha
                .WithMany(ct => ct.SubCriteria) // mỗi tiêu chỉ cha có thể có nhiều tiêu chỉ con
                .HasForeignKey(c => c.ParentID) // khóa ngoại trỏ đến tiêu chỉ cha
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
