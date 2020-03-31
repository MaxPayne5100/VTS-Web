using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VTS.DAL.Entities;

namespace VTS.DAL.Configuration
{
    public class ManagerEntityConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Subordinates)
                .WithOne(x => x.Manager)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}