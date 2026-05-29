using System.Security.Claims;

using AspNet.Security.OAuth.Discord;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Gsri.Personnels;

internal static class SecurityConfiguration
{
    private const string DiscordAuthenticationConfiguration = "Authentication:Schemes:Discord";
    private const string WhitelistConfigurationPath = "Authorization:Whitelist";

    extension(WebApplicationBuilder webApplicationBuilder)
    {
        public void AddSecurity()
        {
            webApplicationBuilder.Services.AddAuthentication(AddAuthentication).AddCookie().AddDiscord();
            webApplicationBuilder.Services.AddOptions<DiscordAuthenticationOptions>(DiscordAuthenticationDefaults.AuthenticationScheme).BindConfiguration(DiscordAuthenticationConfiguration);
            webApplicationBuilder.Services.AddAuthorizationBuilder().AddDefaultPolicy("", webApplicationBuilder.WhitelistPolicy);
        }

        private string[] Whitelist => webApplicationBuilder.Configuration.GetSection(WhitelistConfigurationPath).Get<string[]>() ?? [];
        private AuthorizationPolicy WhitelistPolicy => new AuthorizationPolicyBuilder().RequireClaim(ClaimTypes.NameIdentifier, webApplicationBuilder.Whitelist).Build();

    }

    internal static void AddAuthentication(AuthenticationOptions options)
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
    }
}