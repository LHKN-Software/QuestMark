using Markdig.Renderers;
using Markdig.Syntax.Inlines;

namespace QuestMark.Renderers.Inlines;

internal class HtmlEntityInlineRenderer : MarkdownObjectRenderer<PdfRenderer, HtmlEntityInline>
{
    protected override void Write(PdfRenderer renderer, HtmlEntityInline entity) { }
}
