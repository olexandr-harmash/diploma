using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace diploma.Network;

public class StrategyNotFoundException : Exception
{
    public StrategyNotFoundException(string message) : base(message)
    {
    }

    public static void ThrowIfNull([NotNull] object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
        {
            Throw(paramName);
        }
    }

    [DoesNotReturn]
    internal static void Throw(string? paramName) =>
            throw new ArgumentNullException(paramName);
}
