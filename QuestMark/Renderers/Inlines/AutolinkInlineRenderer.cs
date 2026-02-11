using Markdig.Renderers;
using Markdig.Syntax.Inlines;
using QuestMark.Extensions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace QuestMark.Renderers.Inlines;

public class AutolinkInlineRenderer : MarkdownObjectRenderer<PdfRenderer, AutolinkInline>
{
    protected override void Write(PdfRenderer renderer, AutolinkInline autolink)
    {
        TextDescriptor text = renderer.CurrentText.ThrowIfNull();
        text.Hyperlink(autolink.Url, autolink.Url).FontColor(Colors.Blue.Medium);
    }
}
