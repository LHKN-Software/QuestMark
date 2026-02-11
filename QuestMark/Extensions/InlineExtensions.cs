using Markdig.Syntax.Inlines;

namespace QuestMark.Extensions;

public static class InlineExtensions
{
    public static T? GetAncestorOfType<T>(this Inline inline)
        where T : Inline
    {
        ContainerInline? parent = inline.Parent;

        while (parent != null)
        {
            if (parent is T p)
            {
                return p;
            }

            parent = parent.Parent;
        }

        return null;
    }

    public static bool HasAncestorOfType<T>(this Inline inline)
        where T : Inline => inline.GetAncestorOfType<T>() != null;

    public static bool IsBold(this Inline inline)
    {
        EmphasisInline? emphasis = inline.GetAncestorOfType<EmphasisInline>();
        return emphasis?.DelimiterCount == 2;
    }

    public static bool IsItalic(this Inline inline)
    {
        EmphasisInline? emphasis = inline.GetAncestorOfType<EmphasisInline>();
        return emphasis?.DelimiterCount is not null and not 2;
    }
}
