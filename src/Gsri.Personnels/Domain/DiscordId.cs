namespace Gsri.Personnels.Domain;

public record DiscordId(long Value)
{
    public static DiscordId? Factory(long? value) => value is >= 0 ? new(value.Value) : null;
    public static DiscordId? Factory(string? value) => long.TryParse(value, out long result) ? Factory(result) : null;
    public static bool TryParse(string? value, out DiscordId result) => (result = Factory(value)!) is not null;
}
