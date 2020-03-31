using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VTS.DAL.Entities;

namespace VTS.DAL.Configuration
{
    public class HeadEntityConfiguration : IEntityTypeConfiguration<Head>
    {
        public void Configure(EntityTypeBuilder<Head> builder)
        {
            builder.HasKey(x => x.Id);

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