using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VTS.DAL.Entities;

namespace VTS.DAL.Configuration
{
    /// <summary>
    /// Configuration for Clerk Entity.
    /// </summary>
    public class ClerkEntityConfiguration : IEntityTypeConfiguration<Clerk>
    {
        /// <summary>
        /// Configure method.
        /// </summary>
        /// <param name="builder">A param with EntityTypeBuilder type.</param>
        public void Configure(EntityTypeBuilder<Clerk> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}