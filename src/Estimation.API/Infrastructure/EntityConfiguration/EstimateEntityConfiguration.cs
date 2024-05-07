using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace diploma.Estimation.API.Infrastructure.EntityConfiguration;

public class EstimateEntityConfiguration : IEntityTypeConfiguration<Estimate>
{
    public void Configure(EntityTypeBuilder<Estimate> builder)
    {
        builder.Property(p => p.Name).IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.CreatedBy).IsRequired()
            .HasMaxLength(50);

        builder.HasMany(e => e.Criterions)
            .WithOne(ec => ec.Estimate);
    }
}
