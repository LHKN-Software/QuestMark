using Markdig.Renderers;
using Markdig.Syntax.Inlines;
using QuestMark.Extensions;
using QuestPDF.Fluent;

namespace QuestMark.Renderers.Inlines;

internal class DelimiterInlineRenderer : MarkdownObjectRenderer<PdfRenderer, DelimiterInline>
{
    protected override void Write(PdfRenderer renderer, DelimiterInline delimiter)
    {
        TextDescriptor descriptor = renderer.CurrentText.ThrowIfNull();
        descriptor.Span(delimiter.ToLiteral());
    }
}
