using Markdig.Renderers;
using Markdig.Syntax.Inlines;
using QuestMark.Extensions;

namespace QuestMark.Renderers.Inlines;

public class LineBreakInlineRenderer : MarkdownObjectRenderer<PdfRenderer, LineBreakInline>
{
    protected override void Write(PdfRenderer renderer, LineBreakInline lineBreak)
    {
        renderer.CurrentText.ThrowIfNull().EmptyLine();
    }
}
