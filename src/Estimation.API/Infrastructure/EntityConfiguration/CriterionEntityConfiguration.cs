using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace diploma.Estimation.API.Infrastructure.EntityConfiguration;

public class CriterionEntityConfiguration : IEntityTypeConfiguration<Criterion>
{
    public void Configure(EntityTypeBuilder<Criterion> builder)
    {
        builder.Property(pr => pr.Name)
            .HasMaxLength(50);
    }
}
