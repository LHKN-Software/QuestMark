using Markdig.Renderers;
using Markdig.Syntax.Inlines;

namespace QuestMark.Renderers.Inlines;

public class DelimiterInlineRenderer : MarkdownObjectRenderer<PdfRenderer, DelimiterInline>
{
    protected override void Write(PdfRenderer renderer, DelimiterInline delimiter)
    {
        delimiter.ReplaceByLiteral();
    }
}
