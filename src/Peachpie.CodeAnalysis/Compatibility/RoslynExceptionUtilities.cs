namespace Roslyn.Utilities
{
    internal static class ExceptionUtilities
    {
        internal static System.Exception Unreachable => Peachpie.CodeAnalysis.Utilities.ExceptionUtilities.Unreachable;

        internal static System.Exception UnexpectedValue(object o) => new System.InvalidOperationException($"Unexpected value: {o}");
    }
}
