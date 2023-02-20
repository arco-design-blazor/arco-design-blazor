using System.Text;
using System.Text.RegularExpressions;

namespace ArcoDesign.IconGenerator;
public class Program {
    static async Task Main(string[] _) {
        var generator = new IconGenerator();
        await generator.Run();
    }
}

public class IconGenerator {

    private readonly string _fileDir;
    private readonly string _template;
    private readonly string outputDir;

    public IconGenerator() {
        _fileDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "_svgs");
        _template = File.ReadAllText("./IconComponentTemplate.txt");
        outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "blazor");
    }

    private static string GetComponentName(string name) {
        if(name.IndexOf('-') == -1) {
            return $"{char.ToUpper(name[0])}{name[1..]}";
        }

        var s = new StringBuilder();
        foreach (var item in name.Split('-')) {
            _ = s.Append($"{char.ToUpper(item[0])}{item[1..]}");
        }

        return s.ToString();
    }

    public static IEnumerable<SvgFlatData> GetSvgInfo(DirectoryInfo dir) {
        var list = new List<SvgFlatData>();   
        foreach(var file in dir.EnumerateFiles("*.svg")) {
            var fileName = Path.GetFileNameWithoutExtension(file.FullName);
            list.Add(new SvgFlatData {
                ComponentName=$"Icon{GetComponentName(fileName)}",
                FileName = fileName,
                FilePath = file.FullName
            });
        }

        foreach(var dirItem in dir.EnumerateDirectories()) {
            list.AddRange(GetSvgInfo(dirItem));
        }
        return list;
    }


    public async Task Run() {
        var data = GetSvgInfo(new DirectoryInfo(_fileDir));
        foreach(var item in data) {
            var content = await File.ReadAllTextAsync(item.FilePath);
            content = $"{content[..5]} class=\"@classNameBuilder.Build()\" {content[5..]}";

            Console.WriteLine(item.FileName);
            var newIcon = _template.Replace("/SVG/", content)
                .Replace("/ICONNAME/", item.ComponentName)
                .Replace("/ICONCLASSNAME/", item.FileName);
            await File.WriteAllTextAsync(Path.Combine(outputDir, $"{item.ComponentName}.razor"), newIcon);
        }
    }
}

public class SvgFlatData {
    public string FileName { get; set; }
    public string ComponentName { get; set; }
    public string FilePath { get; set; }
}
