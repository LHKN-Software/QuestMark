using Markdig;
using QuestMark.Components;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QuestMark;

public static class Markdown
{
    public static MarkdownComponent ToMarkdownComponent(MarkdownPipeline pipeline, string markdown)
    {
        return new MarkdownComponent(pipeline, markdown);
    }

    public static void WritePdfToStream(MarkdownPipeline pipeline, string markdown, Stream stream)
    {
        Document document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.Margin(10);

                IContainer content = page.Content();
                content.Component(ToMarkdownComponent(pipeline, markdown));
            });
        });

        document.GeneratePdf(stream);
    }
}
