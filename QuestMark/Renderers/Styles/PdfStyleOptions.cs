using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QuestMark.Renderers.Styles;

public record PdfStyleOptions
{
    public static readonly PdfStyleOptions Default = new();

    public TextStyle LinkTextStyle { get; set; } = TextStyle.Default.FontColor(Colors.Blue.Medium);

    public TextStyle CodeTextStyle { get; set; } =
        TextStyle
            .Default.FontFamily("Courier New")
            .BackgroundColor(Colors.Grey.Lighten5)
            .FontColor(Colors.Red.Medium)
            .SemiBold();

    public BlockStyler BlockQuoteStyler { get; set; } =
        container =>
            container
                .PaddingVertical(4)
                .Background(Colors.Grey.Lighten5)
                .BorderLeft(2)
                .BorderColor(Colors.Red.Medium)
                .PaddingHorizontal(10)
                .PaddingVertical(5);

    public BlockStyler ThematicBreakStyler { get; set; } =
        container =>
        {
            container.PaddingVertical(8).LineHorizontal(1).LineColor(Colors.Grey.Medium);
            return container;
        };

    public ListBlockStyler ListStyler { get; set; } =
        (container, depth) => container.PaddingLeft(depth * 2);

    public HeadingStyler HeadingStyler { get; set; } =
        level =>
        {
            Int32 size = 40 - level * 6;
            bool isBold = level > 3;
            TextStyle style = TextStyle.Default.FontSize(size);
            return isBold ? style.Bold() : style;
        };
}
