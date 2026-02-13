using Markdig.Renderers;
using Markdig.Syntax.Inlines;
using QuestMark.Extensions;
using QuestPDF.Fluent;

namespace QuestMark.Renderers.Inlines;

internal class LiteralInlineRenderer : MarkdownObjectRenderer<PdfRenderer, LiteralInline>
{
    protected override void Write(PdfRenderer renderer, LiteralInline literal)
    {
        TextDescriptor? text = renderer.CurrentText.ThrowIfNull();
        LinkInline? link = literal.GetAncestorOfType<LinkInline>();

        TextSpanDescriptor span;

        if (link is null)
        {
            span = text.Span(literal.Content.ToString());
        }
        else
        {
            string url = string.IsNullOrWhiteSpace(link.Url) ? "/" : link.Url;
            span = text.Hyperlink(literal.Content.ToString(), url)
                .Style(renderer.StyleOptions.LinkTextStyle);
        }

        span.Italic(literal.IsItalic());

        if (literal.IsBold())
        {
            span.Bold();
        }
    }
}
