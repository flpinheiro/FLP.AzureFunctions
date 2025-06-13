using FLP.Core.Context.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FLP.Infra.Data.mappings;

internal class BugMapProfile : IEntityTypeConfiguration<Bug>
{
    public void Configure(EntityTypeBuilder<Bug> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title).IsRequired().HasMaxLength(50);
        builder.Property(b => b.Description).IsRequired().HasMaxLength(500);
        builder.Property(b => b.Status).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
        builder.Property(b => b.ResolvedAt).IsRequired(false);
        builder.Property(b => b.AssignedToUserId).IsRequired(false);
    }
}
