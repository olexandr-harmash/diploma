using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace diploma.Projects.API.Infrastructure.EntityConfigurations;

public class ProjectRepositoryEntityConfiguration
    : IEntityTypeConfiguration<ProjectModel>
{
    public void Configure(EntityTypeBuilder<ProjectModel> builder)
    {
        builder.Property(pr => pr.Name).IsRequired()
            .HasMaxLength(50);

        builder.Property(pr => pr.Description).IsRequired()
            .HasMaxLength(200);

        builder.Property(pr => pr.Link).IsRequired();
    }
}
