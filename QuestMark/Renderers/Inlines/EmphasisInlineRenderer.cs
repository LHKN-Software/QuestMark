using Markdig.Renderers;
using Markdig.Syntax.Inlines;

namespace QuestMark.Renderers.Inlines;

internal class EmphasisInlineRenderer : MarkdownObjectRenderer<PdfRenderer, EmphasisInline>
{
    protected override void Write(PdfRenderer renderer, EmphasisInline emphasis)
    {
        renderer.WriteChildren(emphasis);
    }
}
