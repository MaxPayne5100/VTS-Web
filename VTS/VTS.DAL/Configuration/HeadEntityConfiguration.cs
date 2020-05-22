using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VTS.DAL.Entities;

namespace VTS.DAL.Configuration
{
    /// <summary>
    /// Configuration for Head Entity.
    /// </summary>
    public class HeadEntityConfiguration : IEntityTypeConfiguration<Head>
    {
        /// <summary>
        /// Configure method.
        /// </summary>
        /// <param name="builder">A param with EntityTypeBuilder type.</param>
        public void Configure(EntityTypeBuilder<Head> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Manager)
                .WithOne(x => x.Head)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Clerk)
                .WithOne(x => x.Head)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.HolidayAcception)
                .WithOne(x => x.Head)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}