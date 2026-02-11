using Markdig.Renderers;
using Markdig.Syntax.Inlines;
using QuestMark.Extensions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace QuestMark.Renderers.Inlines;

public class LiteralInlineRenderer : MarkdownObjectRenderer<PdfRenderer, LiteralInline>
{
    protected override void Write(PdfRenderer renderer, LiteralInline literal)
    {
        TextDescriptor? text = renderer.CurrentText.ThrowIfNull();
        LinkInline? link = literal.GetAncestorOfType<LinkInline>();

        TextSpanDescriptor span;

        if (link == null)
        {
            span = text.Span(literal.Content.ToString());
        }
        else
        {
            string url = string.IsNullOrWhiteSpace(link.Url) ? "/" : link.Url;
            span = text.Hyperlink(literal.Content.ToString(), url).FontColor(Colors.Blue.Medium);
        }

        span.Italic(literal.IsItalic());

        if (literal.IsBold())
        {
            span.Bold();
        }
    }
}
