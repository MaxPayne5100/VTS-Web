using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VTS.DAL.Entities;

namespace VTS.DAL.Configuration
{
    /// <summary>
    /// Configuration for UserVacationInfo Entity.
    /// </summary>
    public class UserVacationInfoEntityConfiguration : IEntityTypeConfiguration<UserVacationInfo>
    {
        /// <summary>
        /// Configure method.
        /// </summary>
        /// <param name="builder">A param with EntityTypeBuilder type.</param>
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