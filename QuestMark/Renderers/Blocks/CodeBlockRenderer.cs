using Markdig.Helpers;
using Markdig.Renderers;
using Markdig.Syntax;
using QuestMark.Extensions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace QuestMark.Renderers.Blocks;

/// <summary>
/// Renders a markdown code block. Code blocks are leaf blocks and therefore only contain inline
/// containers.
///
/// After writing children, the current text descriptor is set to null.
/// </summary>
internal class CodeBlockRenderer : MarkdownObjectRenderer<PdfRenderer, CodeBlock>
{
    protected override void Write(PdfRenderer renderer, CodeBlock codeBlock)
    {
        ColumnDescriptor previousColumn = renderer.CurrentColumn.ThrowIfNull();
        string language = codeBlock.GetLanguage();
        Int32 indent = codeBlock.GetIndentCount();

        previousColumn
            .Item()
            .Background(Colors.Grey.Lighten5)
            .Padding(10)
            .Text(text =>
            {
                Int32 i = 0;

                foreach (StringLine line in codeBlock.Lines)
                {
                    text.Span(line.ToString()[indent..]).Style(renderer.StyleOptions.CodeTextStyle);

                    if (i < codeBlock.Lines.Count - 1)
                    {
                        text.EmptyLine();
                    }

                    i++;
                }

                if (!codeBlock.IsLastChild())
                {
                    previousColumn.Item().Text(text => text.EmptyLine());
                }
            });
    }
}
