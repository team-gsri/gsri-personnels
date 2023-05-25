using Gsri.Personnels.Domain;

using Microsoft.EntityFrameworkCore;

namespace Gsri.Personnels.Database;

public class PersonnelsDbContext(DbContextOptions options, IConfiguration configuration) : DbContext(options)
{
    public DbSet<Joueur> Joueurs { get; set; }
    public DbSet<Competence> Competences { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlite(configuration.GetConnectionString("Personnels"));
}
