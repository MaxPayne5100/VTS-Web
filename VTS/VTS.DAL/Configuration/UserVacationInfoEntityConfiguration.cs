using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VTS.DAL.Entities;

namespace VTS.DAL.Configuration
{
    public class UserVacationInfoEntityConfiguration : IEntityTypeConfiguration<UserVacationInfo>
    {
        public void Configure(EntityTypeBuilder<UserVacationInfo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PaidDayOffs)
                .IsRequired();

            builder.Property(x => x.BonusPaidDayOffs)
                .IsRequired();

            builder.Property(x => x.UnPaidDayOffs)
                .IsRequired();

            builder.Property(x => x.PaidSickness)
                .IsRequired();

            builder.Property(x => x.UnPaidSickness)
                .IsRequired();

            builder.Property(x => x.StartedWorking)
                .IsRequired();
        }
    }
}