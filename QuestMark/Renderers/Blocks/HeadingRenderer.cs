using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using QuestMark.Extensions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace QuestMark.Renderers.Blocks;

/// <summary>
/// Renders a markdown heading block. Heading blocks are leaf blocks and therefore only contain
/// inline containers.
///
/// After writing children, the current text descriptor is reset to null.
/// </summary>
internal class HeadingRenderer : MarkdownObjectRenderer<PdfRenderer, HeadingBlock>
{
    protected override void Write(PdfRenderer renderer, HeadingBlock heading)
    {
        ColumnDescriptor? previousColumn = renderer.CurrentColumn.ThrowIfNull();
        ContainerInline? items = heading.Inline.ThrowIfNull();

        Int32 level = heading.Level;
        TextStyle style = renderer.StyleOptions.HeadingTextStyler(level);

        previousColumn
            .Item()
            .Column(column =>
            {
                column
                    .Item()
                    .Text(text =>
                    {
                        renderer.CurrentColumn = column;
                        renderer.CurrentText = text;
                        text.DefaultTextStyle(style);
                        renderer.Write(items);

                        if (!heading.IsLastChild())
                        {
                            column.Item().Text(text => text.EmptyLine());
                        }

                        renderer.CurrentText = null;
                        renderer.CurrentColumn = previousColumn;
                    });
            });
    }
}
