using Markdig;
using QuestMark.Components;
using QuestMark.Renderers;
using QuestMark.Renderers.Styles;

namespace QuestMark;

/// <summary>
/// Contains methods to transform markdown into <see cref="MarkdownComponent"/>s
/// </summary>
public static class Markdown
{
    /// <summary>
    /// Converts given markdown string to a QuestPDF component which can then be inserted into a
    /// PDF document. Uses default styling options.
    /// </summary>
    /// <param name="pipeline">Markdown pipeline</param>
    /// <param name="markdown">Markdown string</param>
    /// <returns>A QuestPDF component</returns>
    public static MarkdownComponent ToMarkdownComponent(
        MarkdownPipeline pipeline,
        string markdown
    ) => ToMarkdownComponent(pipeline, markdown, PdfStyleOptions.Default);

    /// <summary>
    /// Converts given markdown string and PDF styling options to a QuestPDF component which can
    /// then be inserted into a PDF document.
    /// </summary>
    /// <param name="pipeline">Markdown pipeline</param>
    /// <param name="markdown">Markdown string</param>
    /// <param name="styleOptions">PDF style options</param>
    /// <returns>A QuestPDF component</returns>
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
