using Gsri.Personnels.Domain;

using Microsoft.Extensions.Caching.Memory;

namespace Gsri.Personnels.RaidHelper;

public class EventRepository(
    IHttpClientFactory httpClientFactory,
    IMemoryCache memoryCache
)
{
    private const string HttpClientName = "RaidHelper";

    public async Task<string> GetInscription(
      RaidHelperId? raidHelperId,
      DiscordId? discordId,
      CancellationToken cancellationToken)
    => (raidHelperId, discordId) switch
    {
        (RaidHelperId, DiscordId) when await HasSignedUp(raidHelperId, discordId, cancellationToken) => "✅",
        (RaidHelperId, DiscordId) => "🟥",
        _ => string.Empty
    };

    private async Task<bool> HasSignedUp(RaidHelperId raidHelperId, DiscordId discordId, CancellationToken cancellationToken)
    => await GetEvent(raidHelperId, cancellationToken) is Event e && e.SignUps.Any(_ => DiscordId.Factory(_.UserId) == discordId);

    private async Task<Event?> GetEvent(
      RaidHelperId raidHelperId,
      CancellationToken cancellationToken)
    {
        return await memoryCache.GetOrCreateAsync(raidHelperId, async entry =>
        {
            if (await FetchEvent(raidHelperId, cancellationToken) is not Event e)
            {
                entry.Dispose();
                return null;
            }

            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
            return e;
        });
    }

    private async Task<Event?> FetchEvent(
      RaidHelperId raidHelperId,
      CancellationToken cancellationToken)
    {
        using var client = httpClientFactory.CreateClient(HttpClientName);
        return await client.GetFromJsonAsync<Event>(raidHelperId.Value.ToString(), cancellationToken);
    }

    internal static void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient(HttpClientName, client => client.BaseAddress = new("https://raid-helper.xyz/api/v4/events/"));
        builder.Services.AddSingleton<EventRepository>();
        builder.Services.AddMemoryCache();
    }
}