using QuestPDF.Infrastructure;

namespace QuestMark.Renderers.Styles;

public delegate IContainer BlockStyler(IContainer container);

public delegate IContainer ListBlockStyler(IContainer container, Int32 depth);

public delegate TextStyle HeadingStyler(Int32 level);
