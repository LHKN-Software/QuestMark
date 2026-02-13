using Markdig.Renderers;
using Markdig.Syntax.Inlines;

namespace QuestMark.Renderers.Inlines;

internal class HtmlInlineRenderer : MarkdownObjectRenderer<PdfRenderer, HtmlInline>
{
    protected override void Write(PdfRenderer renderer, HtmlInline html) { }
}
