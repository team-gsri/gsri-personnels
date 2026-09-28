namespace Gsri.Personnels.Domain;

public record RaidHelperId(long Value)
{
    public static RaidHelperId? Factory(long? value) => value is >= 0 ? new(value.Value) : null;
    public static RaidHelperId? Factory(string? value) => long.TryParse(value, out long result) ? Factory(result) : null;
    public static bool TryParse(string? value, out RaidHelperId result) => (result = Factory(value)!) is not null;
}