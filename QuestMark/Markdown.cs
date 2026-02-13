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
        if (!Settings.DisableWarnings)
        {
            foreach (IMarkdownExtension extension in pipeline.Extensions)
            {
                if (!PdfRenderer.SupportedExtensions.Contains(extension.GetType()))
                {
                    Console.WriteLine(
                        $"[QuestMark~Warning]: Extension '{extension.GetType()}' is not supported "
                            + "by this library, you may experience unpredictable results"
                    );
                }
            }
        }

        return new MarkdownComponent(pipeline, markdown, styleOptions);
    }
}
