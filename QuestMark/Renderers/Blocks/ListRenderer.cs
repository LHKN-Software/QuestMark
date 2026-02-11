using Markdig.Renderers;
using Markdig.Syntax;
using QuestMark.Extensions;
using QuestPDF.Fluent;

namespace QuestMark.Renderers.Blocks;

public class ListRenderer : MarkdownObjectRenderer<PdfRenderer, ListBlock>
{
    protected override void Write(PdfRenderer renderer, ListBlock list)
    {
        ColumnDescriptor previousColumn = renderer.CurrentColumn.ThrowIfNull();
        Int32 depth = list.GetDepth();

        previousColumn
            .Item()
            .PaddingLeft(depth * 2)
            .Column(outerColumn =>
            {
                foreach (ListItemBlock item in list.Cast<ListItemBlock>())
                {
                    outerColumn
                        .Item()
                        .Row(row =>
                        {
                            row.ConstantItem(12)
                                .Text(text =>
                                {
                                    if (list.IsOrdered)
                                    {
                                        text.Span($"{item.Order}{list.OrderedDelimiter}");
                                    }
                                    else
                                    {
                                        text.Span($"{GetBullet(depth)}");
                                    }
                                });

                            row.RelativeItem()
                                .Column(innerColumn =>
                                {
                                    renderer.CurrentColumn = innerColumn;
                                    renderer.Write(item);
                                    renderer.CurrentColumn = previousColumn;
                                });
                        });
                }
            });
    }

    private static string GetBullet(Int32 depth) =>
        (depth % 4) switch
        {
            1 => "•",
            2 => "○",
            3 => "-",
            0 => "‣",
            _ => "•",
        };
}
