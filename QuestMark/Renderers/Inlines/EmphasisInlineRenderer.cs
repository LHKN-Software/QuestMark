using Markdig.Renderers;
using Markdig.Syntax.Inlines;

namespace QuestMark.Renderers.Inlines;

public class EmphasisInlineRenderer : MarkdownObjectRenderer<PdfRenderer, EmphasisInline>
{
    protected override void Write(PdfRenderer renderer, EmphasisInline emphasis)
    {
        renderer.WriteChildren(emphasis);
    }
}
