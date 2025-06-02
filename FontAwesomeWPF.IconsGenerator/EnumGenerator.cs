using Microsoft.CodeAnalysis;
using System;
using System.Text;
using System.Text.Json;

namespace FontAwesomeWPF.IconsGenerator
{
    [Generator]
    public class EnumGenerator : IIncrementalGenerator
    {
        private static string GenerateIcons(string jsonContent)
        {
            // Path to the icons.json file
            JsonDocument iconsData = JsonDocument.Parse(jsonContent);

            var enumBuilder = new StringBuilder();

            enumBuilder.AppendLine("public enum FontAwesomeIconName : ushort");
            enumBuilder.AppendLine("{");

            foreach (var icon in iconsData.RootElement.EnumerateObject())
            {
                string iconName = icon.Name;
                var iconDetails = icon.Value;

                if (!iconDetails.TryGetProperty("unicode", out JsonElement unicodeElement))
                    continue;

                string unicode = unicodeElement.GetString();

                if (string.IsNullOrEmpty(unicode))
                    continue;

                // Convert icon name to PascalCase for enum
                string enumName = ToPascalCase(iconName);
                if (!string.IsNullOrEmpty(enumName) && char.IsDigit(enumName[0]))
                    enumName = "Icon" + enumName;

                enumBuilder.AppendLine($"    {enumName} = 0x{int.Parse(unicode, System.Globalization.NumberStyles.HexNumber):X4},");
            }

            enumBuilder.AppendLine("}");

            // Combine and write to a file
            string namespaceName = "FontAwesomeWPF";
            string output = $"namespace {namespaceName}\n{{\n\t{enumBuilder}\n}}";

            return output;
        }

        private static string ToPascalCase(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var words = text.Split(new[] { '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
            var result = new StringBuilder();

            foreach (var word in words)
            {
                result.Append(char.ToUpperInvariant(word[0]));
                if (word.Length > 1)
                    result.Append(word.Substring(1));
            }

            return result.ToString();
        }

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            IncrementalValuesProvider<AdditionalText> jsonFiles =
                context.AdditionalTextsProvider.Where(static file => file.Path.EndsWith("icons.json"));

            IncrementalValuesProvider<string> sourceCode = jsonFiles.Select(
                (text, cancellationToken) => GenerateIcons(text.GetText(cancellationToken).ToString()));

            context.RegisterSourceOutput(sourceCode, (spc, nameAndContent) =>
                spc.AddSource($"FontAwesomeIcons.generated.cs", nameAndContent));
        }
    }
}