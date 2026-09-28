using System.Text.Json.Serialization;

namespace Gsri.Personnels.RaidHelper;

public record Creator(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("id")] string Id
);
