using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace EFP.RequestList.Libraries.Settings
{
    public static class SettingsManager
    {
        private const string SETTINGS_FILE_NAME = "appsettings.json";

        private static readonly string _path;

        public static DataBaseSettings DataBaseSettings { get; set; } = new DataBaseSettings();

        static SettingsManager()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            try
            {
                _path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) ?? string.Empty;
            }
            catch { }
        }

        public static void LoadSettings()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(_path)
                .AddJsonFile(@SETTINGS_FILE_NAME, optional: false, reloadOnChange: true)
                .Build();

            var dataBaseConfigSection = config.GetSection(nameof(DataBaseSettings));
            ConfigurationBinder.Bind(dataBaseConfigSection, DataBaseSettings);

            Console.WriteLine();
        }

        public static void SaveSettings()
        {
            var path = Path.Combine(_path, SETTINGS_FILE_NAME);
            if(!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            string json = File.Exists(path) ? File.ReadAllText(path) : "{ }";
            dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(json);

            jsonObj[nameof(DataBaseSettings)] = JToken.FromObject(DataBaseSettings);

            var output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(path, output);
        }
    }
}
