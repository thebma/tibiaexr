using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using tibexr.Static;

namespace tibexr.Util
{
    public struct Config
    {
        public int Version;
        public string Path;
    }

    public class CommonFiles
    {
        public static bool HasConfig()
        {
            return File.Exists("config");
        }

        public static void WriteConfig(Config config)
        {
            StringBuilder configNLS = new StringBuilder(); //NLS -> New line seperated.
            configNLS.AppendLine(config.Version.ToString());
            configNLS.AppendLine(config.Path);

            File.WriteAllText("config", configNLS.ToString());
        }

        public static Config ReadConfig()
        {
            string contents = File.ReadAllText("config");
            int indices = contents.ToCharArray().Where(x => x == '\n').Count();

            string[] lines = contents.Replace("\r", string.Empty)
                                     .Split('\n')
                                     .Take(indices)
                                     .ToArray();

            Config config = new Config
            {
                Version = int.Parse(lines[0]),
                Path = lines[1]
            };

            return config;
        }

        public static void PrepareDistro(string shortCode)
        {
            if (!TibiaVersions.IsVersionSupported(shortCode))
            {
                return;
            }

            string path = $"distro/{shortCode}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                DirectoryInfo rootDirectoryInfo = new DirectoryInfo(path);

                foreach (FileInfo fileInfo in rootDirectoryInfo.GetFiles())
                {
                    fileInfo.Delete();
                }

                foreach (DirectoryInfo directoryInfo in rootDirectoryInfo.GetDirectories())
                {
                    directoryInfo.Delete(true);
                }
            }
        }

        public static void PrepareDistroFolder()
        {
            if (!Directory.Exists("distro"))
            {
                Directory.CreateDirectory("distro");
            }
        }
    }
}
