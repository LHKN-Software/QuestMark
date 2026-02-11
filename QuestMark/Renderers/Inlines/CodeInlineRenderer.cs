using Markdig.Renderers;
using Markdig.Syntax.Inlines;
using QuestMark.Extensions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace QuestMark.Renderers.Inlines;

public class CodeInlineRenderer : MarkdownObjectRenderer<PdfRenderer, CodeInline>
{
    protected override void Write(PdfRenderer renderer, CodeInline code)
    {
        TextDescriptor? text = renderer.CurrentText.ThrowIfNull();

        text.Span(code.Content)
            .FontFamily("Andale Mono")
            .FontColor(Colors.Red.Medium)
            .BackgroundColor(Colors.Grey.Lighten4);
    }
}
