using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.PooledObjects;
using Pchp.CodeAnalysis.Errors;

namespace Pchp.CodeAnalysis.CommandLine
{
    internal sealed class PhpGeneratorDriver : GeneratorDriver
    {
        private readonly PhpParseOptions _parseOptions;

        internal PhpGeneratorDriver(
            PhpParseOptions parseOptions,
            ImmutableArray<ISourceGenerator> generators,
            AnalyzerConfigOptionsProvider optionsProvider,
            ImmutableArray<AdditionalText> additionalTexts,
            GeneratorDriverOptions driverOptions)
            : base(parseOptions, generators, optionsProvider, additionalTexts, driverOptions)
        {
            _parseOptions = parseOptions;
        }

        private PhpGeneratorDriver(PhpParseOptions parseOptions, GeneratorDriverState state)
            : base(state)
        {
            _parseOptions = parseOptions;
        }

        internal override CommonMessageProvider MessageProvider => Pchp.CodeAnalysis.Errors.MessageProvider.Instance;

        internal override GeneratorDriver FromState(GeneratorDriverState state) => new PhpGeneratorDriver(_parseOptions, state);

        internal override SyntaxTree ParseGeneratedSourceText(GeneratedSourceText input, string fileName, CancellationToken cancellationToken)
            => PhpSyntaxTree.ParseCode(input.Text, _parseOptions, _parseOptions.WithKind(SourceCodeKind.Script), fileName);

        internal override string SourceExtension => ".php";

        internal override string EmbeddedAttributeDefinition => string.Empty;

        internal override ISyntaxHelper SyntaxHelper => PhpSyntaxHelper.Instance;

        private sealed class PhpSyntaxHelper : AbstractSyntaxHelper
        {
            internal static readonly PhpSyntaxHelper Instance = new PhpSyntaxHelper();

            public override bool IsCaseSensitive => true;

            public override bool IsValidIdentifier(string name) => !string.IsNullOrWhiteSpace(name);

            public override string GetUnqualifiedIdentifierOfName(SyntaxNode name) => name?.ToString() ?? string.Empty;

            public override bool IsAnyNamespaceBlock(SyntaxNode node) => false;

            public override bool IsAttribute(SyntaxNode node) => false;

            public override SyntaxNode GetNameOfAttribute(SyntaxNode node) => node;

            public override SyntaxNode RemapAttributeTarget(SyntaxNode target) => target;

            public override SyntaxNode GetAttributeOwningNode(SyntaxNode attribute) => attribute?.Parent ?? attribute;

            public override bool IsAttributeList(SyntaxNode node) => false;

            public override SeparatedSyntaxList<SyntaxNode> GetAttributesOfAttributeList(SyntaxNode node) => default;

            public override void AddAttributeTargets(SyntaxNode node, ArrayBuilder<SyntaxNode> targets)
            {
            }

            public override bool IsLambdaExpression(SyntaxNode node) => false;

            public override void AddAliases(GreenNode node, ArrayBuilder<(string aliasName, string symbolName)> aliases, bool global)
            {
            }

            public override void AddAliases(CompilationOptions options, ArrayBuilder<(string aliasName, string symbolName)> aliases)
            {
            }

            public override bool ContainsGlobalAliases(SyntaxNode root) => false;
        }
    }
}
