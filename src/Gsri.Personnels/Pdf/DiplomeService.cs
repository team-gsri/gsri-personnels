using Gsri.Personnels.Components.Pdf;
using Gsri.Personnels.Domain;

using Microsoft.AspNetCore.Components;

using PeachPDF;

namespace Gsri.Personnels.Pdf;

public class DiplomeService(Microsoft.AspNetCore.Components.Web.HtmlRenderer htmlRenderer)
{
    public async Task<byte[]> GenerateAsync(Qualification qualification)
    {
        var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var parameters = ParameterView.FromDictionary(new Dictionary<string, object?>
            {
                [nameof(DiplomeDocument.Qualification)] = qualification
            });
            var output = await htmlRenderer.RenderComponentAsync<DiplomeDocument>(parameters);
            return output.ToHtmlString();
        });

        var config = new PdfGenerateConfig
        {
            PageSize = PageSize.A4,
            PageOrientation = PageOrientation.Landscape
        };
        config.SetMargins(0);

        var generator = new PdfGenerator();
        await generator.AddDiplomeFontsAsync();

        var document = await generator.GeneratePdf(html, config);
        using var stream = new MemoryStream();
        document.Save(stream);
        return stream.ToArray();
    }
}
