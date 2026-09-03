using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gsri.Personnels.Domain;

public record Participation
{
    public required Guid Key { get; init; }
    public required Operation Operation { get; init; }
    public required Joueur Joueur { get; init; }

    internal static Participation Factory(Joueur joueur, Operation operation) => new()
    {
        Key = Guid.CreateVersion7(),
        Operation = operation,
        Joueur = joueur
    };

    private sealed class EntityTypeConfiguration : IEntityTypeConfiguration<Participation>
    {
        public void Configure(EntityTypeBuilder<Participation> builder)
        {
            builder.Property<int>("Id");
            builder.HasKey("Id");

            builder.HasIndex(_ => _.Key).IsUnique();

            builder
                .HasOne(_ => _.Joueur)
                .WithMany(_ => _.Participations)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(_ => _.Operation)
                .WithMany(_ => _.Participations)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}