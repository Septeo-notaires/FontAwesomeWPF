using Microsoft.CodeAnalysis;
using System;
using System.IO;
using System.Text;

namespace FontAwesomeWPF.IconsGenerator
{
    [Generator]
    public class IconsGenerator : IIncrementalGenerator
    {
//        private void GenerateIcons()
//        {
//            // Path to the icons.json file
//            string jsonFilePath = "icons.json"; // Ensure this path is correct
//
//            if (!File.Exists(jsonFilePath))
//            {
//                return;
//            }
//
//            string jsonContent = File.ReadAllText(jsonFilePath);
//            JObject iconsData = JObject.Parse(jsonContent);
//
//            var enumBuilder = new StringBuilder();
//            var dictBuilder = new StringBuilder();
//
//            enumBuilder.AppendLine("public enum FontAwesomeIconName");
//            enumBuilder.AppendLine("{");
//
//            dictBuilder.AppendLine("public static class FontAwesomeIconMap");
//            dictBuilder.AppendLine("{");
//            dictBuilder.AppendLine("    public static readonly Dictionary<FontAwesomeIconName, string> IconMap = new()");
//            dictBuilder.AppendLine("    {");
//
//            foreach (var icon in iconsData)
//            {
//                string iconName = icon.Key;
//                var iconDetails = icon.Value;
//                string unicode = iconDetails["unicode"]?.ToString();
//
//                if (string.IsNullOrEmpty(unicode))
//                    continue;
//
//                // Convert icon name to PascalCase for enum
//                string enumName = ToPascalCase(iconName);
//                if (!string.IsNullOrEmpty(enumName) && char.IsDigit(enumName[0]))
//                    enumName = "_" + enumName;
//
//                enumBuilder.AppendLine($"    {enumName},");
//                dictBuilder.AppendLine($"        [FontAwesomeIconName.{enumName}] = \"\\u{int.Parse(unicode, System.Globalization.NumberStyles.HexNumber):X4}\",");
//            }
//
//            enumBuilder.AppendLine("}");
//            dictBuilder.AppendLine("    };");
//            dictBuilder.AppendLine("}");
//
//            // Combine and write to a file
//            string namespaceName = "FontAwesomeWPF.IconsGenerator";
//            string output =
//    $@"using System.Collections.Generic;
//namespace {namespaceName}
//{{
//    {enumBuilder}
//    {dictBuilder}
//}}";
//
//            // Use path from args[0] if provided, else default to project root
//            string projectRoot;
//            string outputPath;
//            projectRoot = AppContext.BaseDirectory;
//            while (!File.Exists(Path.Combine(projectRoot, "FontAwsomeGenerator.csproj")) && Directory.GetParent(projectRoot) != null)
//            {
//                projectRoot = Directory.GetParent(projectRoot)!.FullName;
//            }
//
//            outputPath = Path.Combine(projectRoot, "FontAwesomeIcons.generated.cs");
//
//            File.WriteAllText(outputPath, output);
//        }
//
//        private static string ToPascalCase(string text)
//        {
//            if (string.IsNullOrEmpty(text))
//                return text;
//
//            var words = text.Split(new[] { '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
//            var result = new StringBuilder();
//
//            foreach (var word in words)
//            {
//                result.Append(char.ToUpperInvariant(word[0]));
//                if (word.Length > 1)
//                    result.Append(word.Substring(1));
//            }
//
//            return result.ToString();
//        }

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            // find all additional files that end with .json
            IncrementalValuesProvider<AdditionalText> jsonFiles = context.AdditionalTextsProvider.Where(static file => file.Path.EndsWith(".json"));

            // read their contents and save their name
            IncrementalValuesProvider<string> namesAndContents = jsonFiles.Select((text, cancellationToken) => text.GetText(cancellationToken)!.ToString());

            // generate a class that contains their values as const strings
            context.RegisterSourceOutput(namesAndContents, (spc, nameAndContent) =>
            {
                spc.AddSource($"FontAwesomeIcons", $@"
                    public partial class FontAwesomeIcons
                    {{
                        public string FontAwesomeIcons = ""{nameAndContent}"";
                    }}");
            });
        }
    }
}