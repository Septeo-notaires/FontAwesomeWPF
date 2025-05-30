//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Linq;
//using System.Text.Json.Nodes;
//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.Text;

//namespace FontAwesomeWPF.IconsGenerator
//{
//    [Generator]
//    public class IconsGenerator2 : IIncrementalGenerator
//    {
//        public void Initialize(IncrementalGeneratorInitializationContext context)
//        {
//            // Find all additional files named icons.json
//            var jsonFiles = context.AdditionalTextsProvider
//                .Where(static file => file.Path.EndsWith("icons.json"));

//            // Read the file content
//            var iconsData = jsonFiles.Select((file, ct) =>
//            {
//                var text = file.GetText(ct);
//                return text?.ToString();
//            });

//            context.RegisterSourceOutput(iconsData, (spc, jsonContent) =>
//            {
//                if (string.IsNullOrEmpty(jsonContent))
//                    return;

//                try
//                {
//                    var jsonNode = JsonNode.Parse(jsonContent);
//                    if (jsonNode == null)
//                        return;

//                    var enumBuilder = new StringBuilder();
//                    var dictBuilder = new StringBuilder();

//                    enumBuilder.AppendLine("public enum FontAwesomeIconName");
//                    enumBuilder.AppendLine("{");

//                    dictBuilder.AppendLine("public static class FontAwesomeIconMap");
//                    dictBuilder.AppendLine("{");
//                    dictBuilder.AppendLine("    public static readonly Dictionary<FontAwesomeIconName, string> IconMap = new()");
//                    dictBuilder.AppendLine("    {");

//                    foreach (var (iconName, iconDetailsNode) in jsonNode.AsObject())
//                    {
//                        var iconDetails = iconDetailsNode?.AsObject();
//                        if (iconDetails == null || !iconDetails.TryGetPropertyValue("unicode", out var unicodeNode))
//                            continue;

//                        var unicode = unicodeNode?.ToString();
//                        if (string.IsNullOrEmpty(unicode))
//                            continue;

//                        var enumName = ToPascalCase(iconName);
//                        if (!string.IsNullOrEmpty(enumName) && char.IsDigit(enumName[0]))
//                            enumName = "_" + enumName;

//                        enumBuilder.AppendLine($"    {enumName},");
//                        dictBuilder.AppendLine($"        [FontAwesomeIconName.{enumName}] = \"\\u{int.Parse(unicode, System.Globalization.NumberStyles.HexNumber):X4}\",");
//                    }

//                    enumBuilder.AppendLine("}");
//                    dictBuilder.AppendLine("    };");
//                    dictBuilder.AppendLine("}");

//                    var combinedCode = $@"
//using System.Collections.Generic;

//namespace FontAwesomeWPF.IconsGenerator
//{{
//    {enumBuilder}

//    {dictBuilder}
//}}";

//                    spc.AddSource("FontAwesomeIcons.generated.cs", SourceText.From(combinedCode, Encoding.UTF8));
//                }
//                catch (Exception ex)
//                {
//                    spc.ReportDiagnostic(Diagnostic.Create(
//                        new DiagnosticDescriptor(
//                            "FAWGEN001",
//                            "Error generating FontAwesome icons",
//                            $"Error: {ex.Message}",
//                            "FontAwesomeGenerator",
//                            DiagnosticSeverity.Warning,
//                            true),
//                        Location.None));
//                }
//            });
//        }

//        private static string ToPascalCase(string text)
//        {
//            if (string.IsNullOrEmpty(text))
//                return text;

//            var words = text.Split(new[] { '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
//            var result = new StringBuilder();

//            foreach (var word in words)
//            {
//                result.Append(char.ToUpperInvariant(word[0]));
//                if (word.Length > 1)
//                    result.Append(word.Substring(1));
//            }

//            return result.ToString();
//        }
//    }
//}