using Markdig;
using QuestMark.Components;
using QuestMark.Renderers;
using QuestMark.Renderers.Styles;

namespace QuestMark;

public static class Markdown
{
    public static MarkdownComponent ToMarkdownComponent(
        MarkdownPipeline pipeline,
        string markdown
    ) => ToMarkdownComponent(pipeline, markdown, PdfStyleOptions.Default);

    public static MarkdownComponent ToMarkdownComponent(
        MarkdownPipeline pipeline,
        string markdown,
        PdfStyleOptions styleOptions
    )
    {
        foreach (IMarkdownExtension extension in pipeline.Extensions)
        {
            if (!PdfRenderer.SupportedExtensions.Contains(extension.GetType()))
            {
                throw new InvalidOperationException(
                    $"Extension '{extension.GetType()}' is not supported by this library"
                );
            }
        }

        return new MarkdownComponent(pipeline, markdown, styleOptions);
    }
}
