using Microsoft.EntityFrameworkCore;
using VTS.DAL.Configuration;
using VTS.DAL.Entities;

namespace VTS.DAL
{
    /// <summary>
    /// VTS DbContext.
    /// </summary>
    public class VTSDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VTSDbContext"/> class.
        /// </summary>
        /// <param name="options">Param with DbContextOptions type.</param>
        public VTSDbContext(DbContextOptions<VTSDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Method to configure the model.
        /// </summary>
        /// <param name="modelBuilder">Param with ModelBuilder type.</param>
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
        }

        /// <summary>
        /// Gets or sets DbSet of Users Entities.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets DbSet of Employees Entities.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Gets or sets DbSet of Heads Entities.
        /// </summary>
        public DbSet<Head> Heads { get; set; }

        /// <summary>
        /// Gets or sets DbSet of Managers Entities.
        /// </summary>
        public DbSet<Manager> Managers { get; set; }

        /// <summary>
        /// Gets or sets DbSet of Clerks Entities.
        /// </summary>
        public DbSet<Clerk> Clerks { get; set; }

        /// <summary>
        /// Gets or sets DbSet of UsersVacationInfo Entities.
        /// </summary>
        public DbSet<UserVacationInfo> UsersVacationInfo { get; set; }

        /// <summary>
        /// Gets or sets DbSet of Holidays Entities.
        /// </summary>
        public DbSet<Holiday> Holidays { get; set; }

        /// <summary>
        /// Gets or sets DbSet of HolidaysAcception Entities.
        /// </summary>
        public DbSet<HolidayAcception> HolidaysAcception { get; set; }
    }
}