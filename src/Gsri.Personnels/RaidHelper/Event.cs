using System.Text.Json.Serialization;

namespace Gsri.Personnels.RaidHelper;

public record Event(
    [property: JsonPropertyName("date")] string Date,
    [property: JsonPropertyName("signUps")] IReadOnlyList<SignUp> SignUps,
    [property: JsonPropertyName("color")] string Color,
    [property: JsonPropertyName("classes")] IReadOnlyList<Class> Classes,
    [property: JsonPropertyName("roles")] IReadOnlyList<object> Roles,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("channelType")] string ChannelType,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("templateId")] string TemplateId,
    [property: JsonPropertyName("serverId")] string ServerId,
    [property: JsonPropertyName("leaderId")] string LeaderId,
    [property: JsonPropertyName("lastUpdated")] int? LastUpdated,
    [property: JsonPropertyName("displayTitle")] string DisplayTitle,
    [property: JsonPropertyName("closingTime")] int? ClosingTime,
    [property: JsonPropertyName("advancedSettings")] AdvancedSettings AdvancedSettings,
    [property: JsonPropertyName("startTime")] int? StartTime,
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("channelId")] string ChannelId,
    [property: JsonPropertyName("creator")] Creator Creator,
    [property: JsonPropertyName("coLeaders")] IReadOnlyList<object> CoLeaders,
    [property: JsonPropertyName("leaderName")] string LeaderName,
    [property: JsonPropertyName("channelName")] string ChannelName,
    [property: JsonPropertyName("time")] string Time,
    [property: JsonPropertyName("endTime")] int? EndTime,
    [property: JsonPropertyName("announcements")] IReadOnlyList<object> Announcements
);
