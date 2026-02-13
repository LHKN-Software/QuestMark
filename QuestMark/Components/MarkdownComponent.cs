using Markdig;
using Markdig.Parsers;
using Markdig.Syntax;
using QuestMark.Renderers;
using QuestMark.Renderers.Styles;
using QuestPDF.Infrastructure;

namespace QuestMark.Components;

/// <summary>
/// QuestPDF component which parses and renders markdown
/// </summary>
/// <param name="pipeline">the markdown pipeline to use</param>
/// <param name="markdown">the markdown to parse</param>
/// <param name="styleOptions">the style options to apply to the rendered output</param>
public class MarkdownComponent(
    MarkdownPipeline pipeline,
    string markdown,
    PdfStyleOptions? styleOptions = default
) : IComponent
{
    public void Compose(IContainer container)
    {
        MarkdownDocument document = MarkdownParser.Parse(text: markdown, pipeline: pipeline);
        PdfRenderer renderer = new(container, styleOptions);
        renderer.Render(document);
    }
}
