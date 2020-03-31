using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VTS.DAL.Entities;

namespace VTS.DAL.Configuration
{
    public class HolidayAcceptionEntityConfiguration : IEntityTypeConfiguration<HolidayAcception>
    {
        public void Configure(EntityTypeBuilder<HolidayAcception> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}