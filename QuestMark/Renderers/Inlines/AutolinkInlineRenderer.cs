using Markdig.Renderers;
using Markdig.Syntax.Inlines;
using QuestMark.Extensions;
using QuestPDF.Fluent;

namespace QuestMark.Renderers.Inlines;

internal class AutolinkInlineRenderer : MarkdownObjectRenderer<PdfRenderer, AutolinkInline>
{
    protected override void Write(PdfRenderer renderer, AutolinkInline autolink)
    {
        TextDescriptor text = renderer.CurrentText.ThrowIfNull();
        text.Hyperlink(autolink.Url, autolink.Url).Style(renderer.StyleOptions.LinkTextStyle);
    }
}
