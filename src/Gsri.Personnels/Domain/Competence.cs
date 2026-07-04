using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gsri.Personnels.Domain;

public record Competence
{
    public required string Name { get; init; }
    public required Duree Duree { get; init; }
    public ICollection<Qualification> Qualifications { get; init; } = [];

    public static Competence? Factory(string? name, int? duree)
    => (name, Duree.Factory(duree)) is (not null and not "", not null) result ? new() { Name = name, Duree = result.Item2 } : null;

    private sealed class EntityTypeConfiguration : IEntityTypeConfiguration<Competence>
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

public static partial class DomainExtensions
{
    extension(IQueryable<Competence> competences)
    {
        public Task<Competence?> ByName(string name) => competences.FirstOrDefaultAsync(_ => _.Name == name);
        public IQueryable<Competence> WhereName(string name) => competences.Where(_ => _.Name == name);
    }
}