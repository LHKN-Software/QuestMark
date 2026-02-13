using Markdig.Extensions.Abbreviations;
using Markdig.Extensions.AutoLinks;
using Markdig.Extensions.EmphasisExtras;
using Markdig.Extensions.Hardlines;
using Markdig.Extensions.ListExtras;
using Markdig.Extensions.NonAsciiNoEscape;
using Markdig.Renderers;
using Markdig.Syntax;
using QuestMark.Renderers.Blocks;
using QuestMark.Renderers.Inlines;
using QuestMark.Renderers.Styles;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace QuestMark.Renderers;

internal class PdfRenderer : RendererBase
{
    internal static readonly HashSet<Type> SupportedExtensions = new([
        typeof(AbbreviationExtension),
        typeof(AutoLinkExtension),
        typeof(EmphasisExtraExtension),
        typeof(ListExtraExtension),
        typeof(NonAsciiNoEscapeExtension),
        typeof(SoftlineBreakAsHardlineExtension),
    ]);

    internal ColumnDescriptor? CurrentColumn { get; set; }

    internal TextDescriptor? CurrentText { get; set; }

    internal PdfStyleOptions StyleOptions { get; set; }

    private readonly IContainer _container;

    public PdfRenderer(IContainer container)
    {
        _container = container;
        StyleOptions = PdfStyleOptions.Default;

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
