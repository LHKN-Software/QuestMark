# QuestMark

This library allows you to render markdown to PDF. It uses the [Markdig](https://github.com/xoofx/markdig) library to parse the markdown and the [QuestPDF](https://www.questpdf.com) library to render the PDF component, hence the name.

The code in this library is heavily inspired by the HTML renderer found in the Markdig source code.

**Note: This is extremely experimental!**

## Getting Started

This library uses the lovely Markdig and QuestPDF libraries to render simple markdown into a QuestPDF component. Most of the time you will be hand-crafting your PDFs using QuestPDF with fairly predictable and nearly static content, but sometimes you might have the need to render some arbitrary markdown into a component inside your document without it appearing ugly and unformatted. That's where QuestMark comes in.

### Installation

QuestMark is available as a NuGet package. Otherwise, simply copy the parts you need - I'm not fussed.

#### Command line

```bash
dotnet add package QuestMark
```

### Usage

Here's an example taken from this repository:

```csharp
// Program.cs

using Markdig;
using QuestMark.Components;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

// Select the approprate QuestPDF license for your usage:
QuestPDF.Settings.License = LicenseType.Community;

// Build the Markdig pipeline:
MarkdownPipeline pipeline = new MarkdownPipelineBuilder()
    .UseAutoLinks()
    .UseNonAsciiNoEscape()
    .UseListExtras()
    .UseAbbreviations()
    .UseEmphasisExtras()
    .UseSoftlineBreakAsHardlineBreak()
    .DisableHtml()
    .Build();

string markdown = File.ReadAllText("./your-file.md");

// Construct the MarkdownComponent (similar API to Markdig):
MarkdownComponent component = QuestMark.Markdown.ToMarkdownComponent(pipeline, markdown);

// Create QuestPDF document:
Document document = Document.Create(document =>
{
    document.Page(page =>
    {
        page.Size(PageSizes.A4);
        page.PageColor(Colors.White);
        page.Margin(10);

        // Add component to page:
        IContainer content = page.Content();
        content.Component(component);
    });
});

bool imUsingTheQuestPDFCompanionApp = true;

if (imUsingTheQuestPDFCompanionApp)
{
    document.ShowInCompanion();
}
else
{
    document.GeneratePdfAndShow();
}
```

## Customizing Renderer

There is basic support for customizing each renderer. You will need to create a `PdfStyleOptions` object and set any of the delegate functions found there with your own.

For example:

```csharp
// Override default styling options for headings and lists:
PdfStyleOptions styleOptions = new()
{
    HeadingTextStyler = (level) =>
    {
        Int32 size = 40 - level * 4;
        TextStyle style = TextStyle.Default.FontSize(size).FontColor(Colors.Grey.Lighten1);

        if (level < 2 && level >= 4)
        {
            style.SemiBold();
        }
        else if (level < 4)
        {
            style.Bold();
        }

        return style;
    },
    ListStyler = (container, depth) => container.PaddingLeft(depth * 4),
};

// Construct the MarkdownComponent (similar API to Markdig):
MarkdownComponent component = QuestMark.Markdown.ToMarkdownComponent(
    pipeline,
    markdown,
    styleOptions // pass it in here
);
```

## To Do

- [ ] render images instead of rendering a link to the image
- [ ] support as many Markdig extensions as possible
  - [ ] tables
  - [ ] alerts
  - [ ] footnotes
  - [ ] JIRA links
