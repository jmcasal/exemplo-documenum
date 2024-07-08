
using Newtonsoft.Json;

namespace examples.common;

public class Settings
{
    public Settings()

    {
        var __index = Environment.GetCommandLineArgs().Skip(1).FirstOrDefault();
        Index = int.TryParse(__index, out var _index) ? _index : 0;
    }

    public string AppName { get; set; }

    public int Index { get; set; }

    public string RedisConnectionString { get; set; }

    public static Settings LoadFromSettings(string fileName)
    {


        var fn = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        var gn = fn.Replace(".json", ".Local.json");

        if (System.IO.File.Exists(gn))
        {
            var json = File.ReadAllText(gn);
            var result = JsonConvert.DeserializeObject<Settings>(json);
            return result;
        }

        if (System.IO.File.Exists(fn))
        {
            var json = File.ReadAllText(fn);
            var result = JsonConvert.DeserializeObject<Settings>(json);
            return result;
        }

        throw new FileNotFoundException($"File {fn} not found");
    }
}
