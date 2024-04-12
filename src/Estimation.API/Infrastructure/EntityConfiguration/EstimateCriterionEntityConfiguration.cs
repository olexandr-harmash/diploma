using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace diploma.Estimation.API.Infrastructure.EntityConfiguration;

public class EstimateCriterionEntityConfiguration : IEntityTypeConfiguration<EstimateCriterion>
{
    public void Configure(EntityTypeBuilder<EstimateCriterion> builder)
    {
        builder.HasOne(pc => pc.Criterion)
            .WithMany()
            .HasForeignKey(pc => pc.CriterionId)
            .IsRequired();
    }
}
