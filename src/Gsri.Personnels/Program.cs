using Gsri.Personnels;
using Gsri.Personnels.Components;
using Gsri.Personnels.Database;
using Gsri.Personnels.Domain;
using Gsri.Personnels.Pdf;

using Microsoft.EntityFrameworkCore;

using PdfSharpCore.Fonts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddDbContextFactory<PersonnelsDbContext>();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddScoped<Microsoft.AspNetCore.Components.Web.HtmlRenderer>();
builder.Services.AddScoped<DiplomeService>();
builder.AddSecurity();

GlobalFontSettings.FontResolver = new DiplomeFontResolver();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.MapGet(
    "/qualifications/{key:guid}/diplome.pdf", async (
        Guid key,
        IDbContextFactory<PersonnelsDbContext> dbContextFactory,
        DiplomeService diplomeService,
        HttpContext httpContext) =>
{
    await using var context = await dbContextFactory.CreateDbContextAsync();
    if (await context.Qualifications.AsNoTracking()
        .Include(_ => _.Joueur)
        .Include(_ => _.Competence)
        .ByKey(key) is not Qualification qualification)
    {
        return Results.NotFound();
    }

    var pdf = await diplomeService.GenerateAsync(qualification);
    var filename = $"diplome-{qualification.Joueur.Pseudonyme}-{qualification.Competence.Name}.pdf";
    var disposition = $@"inline; filename=""{filename}""";
    httpContext.Response.Headers.ContentDisposition = disposition;
    return Results.File(pdf, "application/pdf");
    
}).RequireAuthorization();

await app.RunAsync();
