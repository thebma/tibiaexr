using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
    }
}
