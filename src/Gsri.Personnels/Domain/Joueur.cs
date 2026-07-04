using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gsri.Personnels.Domain;

public record Joueur
{
    public required string Pseudonyme { get; init; }
    public ICollection<Qualification> Qualifications { get; init; } = [];

    public static Joueur? Factory(string pseudonyme) => new() { Pseudonyme = pseudonyme, Qualifications = [] };

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

    private sealed class EntityTypeConfiguration : IEntityTypeConfiguration<Joueur>
    {
        public void Configure(EntityTypeBuilder<Joueur> builder)
        {
            builder.Property<int>("Id");
            builder.HasKey("Id");

            builder.HasIndex(_ => _.Pseudonyme).IsUnique();
        }
    }
}

public static partial class DomainExtensions
{
    extension(IQueryable<Joueur> joueurs)
    {
        public Task<Joueur?> ByPseudo(string pseudonyme) => joueurs.FirstOrDefaultAsync(_ => _.Pseudonyme == pseudonyme);
        public IQueryable<Joueur> WherePseudo(string pseudonyme) => joueurs.Where(_ => _.Pseudonyme == pseudonyme);
    }
}