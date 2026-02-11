using Markdig.Renderers;
using Markdig.Syntax.Inlines;

namespace QuestMark.Renderers.Inlines;

public class LinkInlineRenderer : MarkdownObjectRenderer<PdfRenderer, LinkInline>
{
    protected override void Write(PdfRenderer renderer, LinkInline link)
    {
        renderer.WriteChildren(link);
    }
}
