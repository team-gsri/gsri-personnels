using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gsri.Personnels.Domain;

public record Qualification
{
    public required Guid Key { get; init; }
    public DateOnly From { get; init; }
    public DateOnly Until { get; set; }
    public required Joueur Joueur { get; init; }
    public required Competence Competence { get; init; }

    public static Qualification Factory(Joueur joueur, Competence competence, TimeProvider timeProvider) => new()
    {
        Key = Guid.CreateVersion7(),
        Competence = competence,
        Joueur = joueur,
        From = DateOnly.FromDateTime(timeProvider.GetLocalNow().Date),
        Until = DateOnly.FromDateTime(timeProvider.GetLocalNow().Date).AddDays(competence.Duree)
    };

    public bool IsValid(TimeProvider timeProvider) => Today(timeProvider) is DateOnly today && From <= today && today <= Until;
    private static DateOnly Today(TimeProvider timeProvider) => DateOnly.FromDateTime(timeProvider.GetLocalNow().Date);

    private class EntityTypeConfiguration : IEntityTypeConfiguration<Qualification>
    {
        public void Configure(EntityTypeBuilder<Qualification> builder)
        {
            builder.Property<int>("Id");
            builder.HasKey("Id");

            builder.HasIndex(_ => _.Key).IsUnique();

            builder
                .HasOne(_ => _.Joueur)
                .WithMany(_ => _.Qualifications)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(_ => _.Competence)
                .WithMany(_ => _.Qualifications)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}