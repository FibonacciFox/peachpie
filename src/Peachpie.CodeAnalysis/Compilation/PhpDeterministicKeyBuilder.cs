using System;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace Pchp.CodeAnalysis
{
    internal sealed class PhpDeterministicKeyBuilder : DeterministicKeyBuilder
    {
        internal static readonly PhpDeterministicKeyBuilder Instance = new();

        private PhpDeterministicKeyBuilder()
        {
        }

        protected override void WriteCompilationOptionsCore(JsonWriter writer, CompilationOptions options)
        {
            if (options is not PhpCompilationOptions phpOptions)
            {
                throw new ArgumentException(null, nameof(options));
            }

            base.WriteCompilationOptionsCore(writer, options);

            writer.Write("baseDirectory", phpOptions.BaseDirectory);
            writer.Write("subDirectory", phpOptions.SubDirectory);
            writer.Write("sdkDirectory", phpOptions.SdkDirectory);
            writer.Write("targetFramework", phpOptions.TargetFramework);
            writer.Write("embedSourceMetadata", phpOptions.EmbedSourceMetadata);
            writer.Write("versionString", phpOptions.VersionString);
        }

        protected override void WriteParseOptionsCore(JsonWriter writer, ParseOptions parseOptions)
        {
            if (parseOptions is not PhpParseOptions phpOptions)
            {
                throw new ArgumentException(null, nameof(parseOptions));
            }

            base.WriteParseOptionsCore(writer, parseOptions);
            writer.Write("languageVersion", phpOptions.LanguageVersion.ToString());
        }
    }
}
