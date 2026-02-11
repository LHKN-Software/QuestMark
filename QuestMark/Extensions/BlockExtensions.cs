using System.Reflection.Metadata.Ecma335;
using Markdig.Syntax;

namespace QuestMark.Extensions;

public static class BlockExtensions
{
    public static T? GetAncestorOfType<T>(this Block block)
        where T : Block
    {
        ContainerBlock? parent = block.Parent;

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

    public static T? GetParentOfType<T>(this Block block)
        where T : Block
    {
        ContainerBlock? parent = block.Parent;

        if (parent is T p)
        {
            return p;
        }

        return null;
    }

    public static bool IsNested(this Block block)
    {
        ContainerBlock? parent = block.Parent;

        while (parent != null)
        {
            if (parent is ContainerBlock and not MarkdownDocument)
            {
                return true;
            }

            parent = parent.Parent;
        }

        return false;
    }

    public static bool IsLastChild(this Block block) => block.Parent?.LastChild == block;

    public static bool IsFirstChild(this Block block) => block.Parent?.FirstOrDefault() == block;

    public static Int32 GetDepth(this ListBlock list)
    {
        ContainerBlock? parent = list.Parent;
        Int32 depth = 1;

        while (parent != null)
        {
            if (parent is ListBlock)
            {
                depth++;
            }

            parent = parent.Parent;
        }

        return depth;
    }

    public static bool IsListItem(this ParagraphBlock paragraph) =>
        paragraph.Parent is ListItemBlock;

    public static string GetLanguage(this CodeBlock block)
    {
        if (block is FencedCodeBlock fenced)
        {
            return string.IsNullOrWhiteSpace(fenced.Info) ? "plain-text" : fenced.Info;
        }

        return "plain-text";
    }

    public static Int32 GetIndentCount(this CodeBlock block)
    {
        if (block is FencedCodeBlock fenced)
        {
            return fenced.IndentCount;
        }

        return 0;
    }
}
