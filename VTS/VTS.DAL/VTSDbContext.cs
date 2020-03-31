using Microsoft.EntityFrameworkCore;
using VTS.DAL.Configuration;
using VTS.DAL.Entities;
using VTS.DAL.Extensions;

namespace VTS.DAL
{
    public class VTSDbContext : DbContext
    {
        public VTSDbContext(DbContextOptions<VTSDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new HeadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ManagerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ClerkEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserVacationInfoEntityConfiguration());
            modelBuilder.ApplyConfiguration(new HolidayEntityConfiguration());
            modelBuilder.ApplyConfiguration(new HolidayAcceptionEntityConfiguration());

            modelBuilder.Seed();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Head> Heads { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Clerk> Clerks { get; set; }
        public DbSet<UserVacationInfo> UsersVacationInfo { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<HolidayAcception> HolidaysAcception { get; set; }
    }
}