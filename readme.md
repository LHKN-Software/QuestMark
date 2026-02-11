# QuestMark

This library acts as a bridge to render markdown as a [QuestPDF component](https://www.questpdf.com) by using [Markdig](https://github.com/xoofx/markdig) as the parser.

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

// Select the approprate QuestPDF license:
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
Document document = Document.Create(document => {
    document.Page(page => {
        page.Size(PageSizes.A4);
        page.PageColor(Colors.White);
        page.Margin(10);

        IContainer container = page.Content();

        // Add component to page:
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
