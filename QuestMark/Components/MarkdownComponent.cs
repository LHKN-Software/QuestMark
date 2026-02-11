using Markdig;
using Markdig.Parsers;
using Markdig.Syntax;
using QuestMark.Renderers;
using QuestPDF.Infrastructure;

namespace QuestMark.Components;

public class MarkdownComponent(MarkdownPipeline pipeline, string markdown) : IComponent
{
    public void Compose(IContainer container)
    {
        MarkdownDocument document = MarkdownParser.Parse(text: markdown, pipeline: pipeline);
        PdfRenderer renderer = new(container);
        renderer.Render(document);
    }
}
