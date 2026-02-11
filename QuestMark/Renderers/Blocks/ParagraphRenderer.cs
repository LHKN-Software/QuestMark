using System.Reflection.Metadata.Ecma335;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using QuestMark.Extensions;
using QuestPDF.Fluent;

namespace QuestMark.Renderers.Blocks;

public class ParagraphRenderer : MarkdownObjectRenderer<PdfRenderer, ParagraphBlock>
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
        if (!paragraph.IsNested())
        {
            return true;
        }

        if (!paragraph.IsLastChild())
        {
            return true;
        }

        // Always add a new line when inside a list item
        return paragraph.GetParentOfType<ListItemBlock>() != null;
    }
}
