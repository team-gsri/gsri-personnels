using System.Text.Json.Serialization;

namespace Gsri.Personnels.RaidHelper;

public record SignUp(
    [property: JsonPropertyName("cClassName")] string CClassName,
    [property: JsonPropertyName("className")] string ClassName,
    [property: JsonPropertyName("cSpecName")] string CSpecName,
    [property: JsonPropertyName("specEmoteId")] string SpecEmoteId,
    [property: JsonPropertyName("userId")] string UserId,
    [property: JsonPropertyName("cRoleName")] string CRoleName,
    [property: JsonPropertyName("entryTime")] int? EntryTime,
    [property: JsonPropertyName("specName")] string SpecName,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("roleName")] string RoleName,
    [property: JsonPropertyName("roleEmoteId")] string RoleEmoteId,
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("position")] int? Position,
    [property: JsonPropertyName("classEmoteId")] string ClassEmoteId,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("spec2Name")] string Spec2Name,
    [property: JsonPropertyName("cSpec2Name")] string CSpec2Name,
    [property: JsonPropertyName("spec2EmoteId")] string Spec2EmoteId
);
