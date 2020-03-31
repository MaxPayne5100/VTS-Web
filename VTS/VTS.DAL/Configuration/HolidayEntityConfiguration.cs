using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VTS.DAL.Entities;

namespace VTS.DAL.Configuration
{
    public class HolidayEntityConfiguration : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Category)
                .IsRequired();

            builder.Property(x => x.Hours)
                .IsRequired();

            builder.Property(x => x.Start)
                .IsRequired();

            builder.Property(x => x.Expires)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasOne(x => x.HolidayAcception)
                .WithOne(x => x.Holiday)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}