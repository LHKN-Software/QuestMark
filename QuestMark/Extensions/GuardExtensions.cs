using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using QuestPDF.Fluent;

namespace QuestMark.Extensions;

public static class GuardExtensions
{
    [return: NotNull]
    public static T ThrowIfNull<T>(
        [NotNull] this T? obj,
        [CallerArgumentExpression(nameof(obj))] string? paramName = null
    )
    {
        if (obj is null)
        {
            throw new InvalidOperationException(
                $"{paramName ?? nameof(obj)} was not expected to be null"
            );
        }

        return obj;
    }
}
