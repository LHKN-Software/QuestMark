using Markdig.Renderers;
using Markdig.Syntax;
using QuestMark.Extensions;
using QuestPDF.Fluent;

namespace QuestMark.Renderers.Blocks;

internal class ThematicBreakRenderer : MarkdownObjectRenderer<PdfRenderer, ThematicBreakBlock>
{
    protected override void Write(PdfRenderer renderer, ThematicBreakBlock thematicBreak)
    {
        ColumnDescriptor column = renderer.CurrentColumn.ThrowIfNull();
        renderer.StyleOptions.ThematicBreakStyler(column.Item());
        column.Item().Text(text => text.EmptyLine());
    }
}
