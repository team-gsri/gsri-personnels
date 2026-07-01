using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gsri.Personnels.Domain;

public record Competence
{
    public required string Name { get; init; }
    public required Duree Duree { get; init; }
    public ICollection<Qualification> Qualifications { get; init; } = [];

    private class EntityTypeConfiguration : IEntityTypeConfiguration<Competence>
    {
        public void Configure(EntityTypeBuilder<Competence> builder)
        {
            builder.Property<int>("Id");
            builder.HasKey("Id");

            builder.HasIndex(_ => _.Name).IsUnique();
            builder.Property(_ => _.Duree).HasConversion(
                static value => value.Value,
                static value => Duree.Factory(value)!
            );
        }
    }
}
