using Markdig.Renderers;
using Markdig.Syntax;
using QuestMark.Extensions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace QuestMark.Renderers.Blocks;

public class BlockQuoteRenderer : MarkdownObjectRenderer<PdfRenderer, QuoteBlock>
{
    protected override void Write(PdfRenderer renderer, QuoteBlock quoteBlock)
    {
        ColumnDescriptor previousColumn = renderer.CurrentColumn.ThrowIfNull();

        previousColumn
            .Item()
            .PaddingVertical(4)
            .Background(Colors.Grey.Lighten5)
            .BorderLeft(2)
            .BorderColor(Colors.Red.Medium)
            .PaddingHorizontal(10)
            .PaddingVertical(5)
            .Column(col =>
            {
                renderer.CurrentColumn = col;
                renderer.WriteChildren(quoteBlock);
                previousColumn.Item().Text(text => text.EmptyLine());
                renderer.CurrentColumn = previousColumn;
            });
    }
}
