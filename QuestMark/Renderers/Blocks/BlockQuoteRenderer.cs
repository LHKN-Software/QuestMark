using Markdig.Renderers;
using Markdig.Syntax;
using QuestMark.Extensions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace QuestMark.Renderers.Blocks;

public class BlockQuoteRenderer : MarkdownObjectRenderer<PdfRenderer, QuoteBlock>
{
    protected override void Write(PdfRenderer renderer, QuoteBlock quoteBlock)
    {
        ColumnDescriptor previousColumn = renderer.CurrentColumn.ThrowIfNull();
        IContainer container = renderer.StyleOptions.BlockQuoteStyler(previousColumn.Item());

        container.Column(column =>
        {
            renderer.CurrentColumn = column;
            renderer.WriteChildren(quoteBlock);

            if (!quoteBlock.IsLastChild())
            {
                previousColumn.Item().Text(text => text.EmptyLine());
            }

            renderer.CurrentColumn = previousColumn;
        });
    }
}
