using System;
using System.IO;
using tibexr.Util;

using static tibexr.Util.PrettyPrint;

namespace tibexr
{
    class Program
    {
        static int CONFIG_VERSION = 1; //Any alterations to the config stucture should be paired with incrementing this number.
        static Config Config;

        static void Main(string[] args)
        {
            //TODO: Support operations via the args array.
            DoConfigChecks();

            //SprFileHandler sprFileHandler70 = new SprFileHandler(@"data\Tibia7.0.spr");
            //sprFileHandler70.SaveRange(0, 100);

            //SprFileHandler sprFileHandler76 = new SprFileHandler(@"data\Tibia7.6.spr");


            //SprFileComparer spriteComparer = new SprFileComparer(sprFileHandler70, sprFileHandler76, sprFileHandler70);
            //spriteComparer.SaveChanged();

            //Console.WriteLine("Reading .dat file..");
            //DatFileHandler datFileHandler = new DatFileHandler(@"data\Tibia.dat");

            Console.ReadLine();
        }

        private static void DoConfigChecks()
        {
            if (!CommonFiles.HasConfig())
            {
                while (true)
                {
                    PPClear();
                    PPStep("setup");
                    PPFormat("No prior config has been found, what path should we look for tibia versions?");
                    PPFormat("Folder structure should contain '70', '86', '91' to corresponding tibia versions.");

                    PPStep("input");
                    PPFormatInline("Path: ");
                    PPStep("setup");

                    string response = Console.ReadLine();

                    if (string.IsNullOrEmpty(response))
                    {
                        PPFormat("Empty user input, please try again.");
                        PPWait();
                    }

                    if (!Directory.Exists(response))
                    {
                        PPFormat("The submitted directory cannot be found, please try again.");
                        PPWait();
                    }

                    //TODO: Scan for tibia directories.

                    Config = new Config
                    {
                        Version = CONFIG_VERSION,
                        Path = response
                    };

                    CommonFiles.WriteConfig(Config);

                    PPFormat("Succesfully created a new config");
                    break;
                }
            }
            else
            {
                PPStep("setup");
                PPFormat("Found prior config file.");

                Config = CommonFiles.ReadConfig();
            }
        }
    }
}
