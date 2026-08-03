using System.Reflection;

using PdfSharpCore.Fonts;

namespace Gsri.Personnels.Pdf;

internal sealed class DiplomeFontResolver : IFontResolver
{
    public const string FontFamily = "sans-serif";
    private const string RegularFaceName = "OpenSans-Regular";
    private const string BoldFaceName = "OpenSans-Bold";

    public string DefaultFontName => FontFamily;

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    => new(isBold ? BoldFaceName : RegularFaceName);

    public byte[] GetFont(string faceName)
    {
        var resourceName = $"Gsri.Personnels.Fonts.{faceName}.ttf";
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)
            ?? throw new FileNotFoundException($"Police embarquée introuvable : {resourceName}");
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}
