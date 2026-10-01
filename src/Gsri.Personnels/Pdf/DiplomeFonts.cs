using System.Reflection;

using PeachPDF;

namespace Gsri.Personnels.Pdf;

internal static class DiplomeFonts
{
    public const string FontFamily = "Open Sans";

    private static readonly string[] FaceNames = ["OpenSans-Regular", "OpenSans-Bold"];

    public static async Task AddDiplomeFontsAsync(this PdfGenerator generator)
    {
        foreach (var faceName in FaceNames)
        {
            var resourceName = $"Gsri.Personnels.Fonts.{faceName}.ttf";
            await using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)
                ?? throw new FileNotFoundException($"Police embarquée introuvable : {resourceName}");
            await generator.AddFontFromStream(stream);
        }
    }
}
