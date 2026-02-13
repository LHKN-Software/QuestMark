using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using QuestMark.Extensions;
using QuestPDF.Fluent;

namespace QuestMark.Renderers.Blocks;

internal class ParagraphRenderer : MarkdownObjectRenderer<PdfRenderer, ParagraphBlock>
{
    protected override void Write(PdfRenderer renderer, ParagraphBlock paragraph)
    {
        ColumnDescriptor previousColumn = renderer.CurrentColumn.ThrowIfNull();
        TextDescriptor? previousText = renderer.CurrentText;

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

                        ContainerInline? children = paragraph.Inline;

                        if (children != null)
                        {
                            renderer.WriteChildren(children);

                            if (ShouldAddEmptyLine(paragraph))
                            {
                                text.EmptyLine();
                            }
                        }

                        renderer.CurrentColumn = previousColumn;
                        renderer.CurrentText = previousText;
                    });
            });
    }

    private static bool ShouldAddEmptyLine(ParagraphBlock paragraph)
    {
        if (!paragraph.IsLastChild())
        {
            return true;
        }

        // We should always add a new line after a list item, except if it's the last list item in
        // the list AND it's nested inside a quote block. This is because the quote block will have
        // a background colour and there will appear to be too much padding at the bottom.

        ListItemBlock? parent = paragraph.GetParentOfType<ListItemBlock>();

        if (parent is null)
        {
            return false;
        }

        bool isInsideQuote = parent.GetAncestorOfType<QuoteBlock>() is not null;

        if (isInsideQuote && parent.IsLastChild())
        {
            return false;
        }

        return true;
    }
}
