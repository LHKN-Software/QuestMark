using Markdig;
using QuestMark.Components;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

MarkdownPipeline pipeline = new MarkdownPipelineBuilder()
    .UseAutoLinks()
    .UseNonAsciiNoEscape()
    .UseListExtras()
    .UseAbbreviations()
    .UseEmphasisExtras()
    .UseSoftlineBreakAsHardlineBreak()
    .DisableHtml()
    .Build();

string markdown = File.ReadAllText("./test.md");

MarkdownComponent component = QuestMark.Markdown.ToMarkdownComponent(pipeline, markdown);

Document document = Document.Create(document =>
{
    document.Page(page =>
    {
        page.Size(PageSizes.A4);
        page.PageColor(Colors.White);
        page.Margin(10);

        IContainer content = page.Content();
        content.Component(component);
    });
});

document.ShowInCompanion();
