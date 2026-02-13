using Markdig.Renderers;
using Markdig.Syntax.Inlines;

namespace QuestMark.Renderers.Inlines;

/// <summary>
/// This renderer simply writes its children. Children are expected to know whether they are inside
/// a LinkInline and should render themselves appropriately.
///
/// For example see <see cref="LiteralInlineRenderer" />
/// </summary>
internal class LinkInlineRenderer : MarkdownObjectRenderer<PdfRenderer, LinkInline>
{
    protected override void Write(PdfRenderer renderer, LinkInline link)
    {
        renderer.WriteChildren(link);
    }
}
