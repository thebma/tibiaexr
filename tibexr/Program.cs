using System;
using System.IO;
using System.Collections.Generic;

using tibexr.Util;
using tibexr.Static;

namespace tibexr
{
    class Program
    {
        static int CONFIG_VERSION = 1; //Any alterations to the config stucture should be paired with incrementing this number.

        static Config Config;
        static List<TibiaVersion> InstalledVersions = new List<TibiaVersion>();


        static void Main(string[] args)
        {
            DatFileHandler datFileHandler = new DatFileHandler(@"data\Tibia.dat");
            
            Console.ReadLine();
        }

    }
}
