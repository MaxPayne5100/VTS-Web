using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VTS.DAL.Entities;

namespace VTS.DAL.Configuration
{
    public class ClerkEntityConfiguration : IEntityTypeConfiguration<Clerk>
    {
        public void Configure(EntityTypeBuilder<Clerk> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}