using Markdig.Renderers;
using Markdig.Syntax.Inlines;
using QuestMark.Extensions;
using QuestPDF.Fluent;

namespace QuestMark.Renderers.Inlines;

public class CodeInlineRenderer : MarkdownObjectRenderer<PdfRenderer, CodeInline>
{
    protected override void Write(PdfRenderer renderer, CodeInline code)
    {
        TextDescriptor? text = renderer.CurrentText.ThrowIfNull();
        text.Span(code.Content).Style(renderer.StyleOptions.CodeTextStyle);
    }
}
