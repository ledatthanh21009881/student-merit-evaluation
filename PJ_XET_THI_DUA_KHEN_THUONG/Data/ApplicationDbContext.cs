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


    }
}
