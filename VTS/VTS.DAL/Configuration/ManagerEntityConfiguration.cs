using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VTS.DAL.Entities;

namespace VTS.DAL.Configuration
{
    /// <summary>
    /// Configuration for Manager Entity.
    /// </summary>
    public class ManagerEntityConfiguration : IEntityTypeConfiguration<Manager>
    {
        /// <summary>
        /// Configure method.
        /// </summary>
        /// <param name="builder">A param with EntityTypeBuilder type.</param>
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Subordinates)
                .WithOne(x => x.Manager)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}