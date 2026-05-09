using System.Collections.Immutable;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis;

namespace Pchp.CodeAnalysis
{
    internal static class ILBuilderCompatibilityExtensions
    {
        internal static void EmitStringConstant(this ILBuilder builder, string value)
            => builder.EmitStringConstant(value, syntax: null);

        internal static void EmitIntegerSwitchJumpTable(
            this ILBuilder builder,
            KeyValuePair<ConstantValue, object>[] jumpTable,
            object fallThroughLabel,
            LocalOrParameter key,
            Microsoft.Cci.PrimitiveTypeCode keyType)
            => builder.EmitIntegerSwitchJumpTable(jumpTable, fallThroughLabel, key, keyType, syntax: null);

        internal static void EmitArrayBlockInitializer(this ILBuilder builder, ImmutableArray<byte> data, object typeReference, DiagnosticBag diagnostics)
            => builder.EmitArrayBlockInitializer(data, syntaxNode: null);
    }
}
