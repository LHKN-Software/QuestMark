using Markdig.Renderers;
using Markdig.Syntax;
using QuestMark.Renderers.Blocks;
using QuestMark.Renderers.Inlines;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace QuestMark.Renderers;

public class PdfRenderer : RendererBase
{
    public ColumnDescriptor? CurrentColumn { get; set; }

    public TextDescriptor? CurrentText { get; set; }

    private readonly IContainer _container;

    public PdfRenderer(IContainer container)
    {
        _container = container;

        _container.Column(column =>
        {
            CurrentColumn = column;
        });

        // Block renderers:
        ObjectRenderers.Add(new BlockQuoteRenderer());
        ObjectRenderers.Add(new CodeBlockRenderer());
        ObjectRenderers.Add(new HeadingRenderer());
        ObjectRenderers.Add(new ListRenderer());
        ObjectRenderers.Add(new ParagraphRenderer());
        ObjectRenderers.Add(new ThematicBreakRenderer());

        // Inline renderers:
        ObjectRenderers.Add(new AutolinkInlineRenderer());
        ObjectRenderers.Add(new CodeInlineRenderer());
        ObjectRenderers.Add(new DelimiterInlineRenderer());
        ObjectRenderers.Add(new EmphasisInlineRenderer());
        ObjectRenderers.Add(new HtmlEntityInlineRenderer());
        ObjectRenderers.Add(new HtmlInlineRenderer());
        ObjectRenderers.Add(new LineBreakInlineRenderer());
        ObjectRenderers.Add(new LinkInlineRenderer());
        ObjectRenderers.Add(new LiteralInlineRenderer());
    }

    public override object Render(MarkdownObject markdownObject)
    {
        Write(markdownObject);
        return _container;
    }
}
