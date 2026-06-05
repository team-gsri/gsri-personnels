using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gsri.Personnels.Domain;

public record Joueur
{
    public required string Pseudonyme { get; init; }
    public ICollection<Qualification> Qualifications { get; init; } = [];

    public void Qualifier(Competence competence, TimeProvider timeProvider)
    {
        var today = DateOnly.FromDateTime(timeProvider.GetLocalNow().Date);
        var qualification = GetQualification(competence, timeProvider);
        if (qualification?.From == today) { return; }

        qualification = Qualification.Factory(this, competence, timeProvider);
        Qualifications.Add(qualification);
    }

    public Qualification? GetQualification(Competence competence, TimeProvider timeProvider)
    => Qualifications
        .Where(_ => _.Competence.Name == competence.Name)
        .Where(_ => _.IsValid(timeProvider))
        .MaxBy(_ => _.Until);

    private class EntityTypeConfiguration : IEntityTypeConfiguration<Joueur>
    {
        public void Configure(EntityTypeBuilder<Joueur> builder)
        {
            builder.Property<int>("Id");
            builder.HasKey("Id");

            builder.HasIndex(_ => _.Pseudonyme).IsUnique();
            builder.HasMany(_ => _.Qualifications).WithOne(_ => _.Joueur).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
