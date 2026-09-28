using System.Text.Json.Serialization;

namespace Gsri.Personnels.RaidHelper;

public record Spec(
    [property: JsonPropertyName("cName")] string CName,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("roleName")] string RoleName,
    [property: JsonPropertyName("roleEmoteId")] string RoleEmoteId,
    [property: JsonPropertyName("limit")] int? Limit,
    [property: JsonPropertyName("emoteId")] string EmoteId
);
