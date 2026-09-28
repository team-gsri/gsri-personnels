using System.Text.Json.Serialization;

namespace Gsri.Personnels.RaidHelper;

public record Class(
    [property: JsonPropertyName("specs")] IReadOnlyList<Spec> Specs,
    [property: JsonPropertyName("cName")] string CName,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("limit")] int? Limit,
    [property: JsonPropertyName("emoteId")] string EmoteId,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("effectiveName")] string EffectiveName
);
