using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gsri.Personnels.Domain;

public record Operation
{
    public DateOnly When { get; init; }
    public ICollection<Participation> Participations { get; init; } = [];

    public static Operation? Factory(DateOnly when) => new() { When = when };

    private sealed class EntityTypeConfiguration : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {
            builder.Property<int>("Id");
            builder.HasKey("Id");

            builder.HasIndex(_ => _.When).IsUnique();
        }
    }
}

public static partial class DomainExtensions
{
    extension(IQueryable<Operation> operations)
    {
        public Task<Operation?> ByWhen(DateOnly when) => operations.FirstOrDefaultAsync(_ => _.When == when);
        public IQueryable<Operation> WhereWhen(DateOnly when) => operations.Where(_ => _.When == when);
    }
}