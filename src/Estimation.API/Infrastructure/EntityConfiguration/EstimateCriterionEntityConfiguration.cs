using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace diploma.Estimation.API.Infrastructure.EntityConfiguration;

public class EstimateCriterionEntityConfiguration : IEntityTypeConfiguration<EstimateCriterion>
{
    public void Configure(EntityTypeBuilder<EstimateCriterion> builder)
    {
        builder.HasOne(ec => ec.Estimate)
            .WithMany(e => e.Criterions);

        builder.HasOne(ec => ec.Criterion)
            .WithMany();
    }
}
