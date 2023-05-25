namespace Gsri.Personnels.Domain;

public record Duree(int Value)
{
    public static implicit operator int(Duree duree) => duree.Value;
    public static Duree? Factory(int? value) => value is > 0 ? new(value.Value) : null;
    public static Duree? Factory(string? value) => int.TryParse(value, out int result) ? Factory(result) : null;
    public override string ToString() => Value.ToString();
}
