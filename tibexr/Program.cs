using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using tibexr.Static;
using tibexr.Util;

using static tibexr.Util.PrettyPrint;

namespace tibexr
{
    class Program
    {
        static int CONFIG_VERSION = 1; //Any alterations to the config stucture should be paired with incrementing this number.
        static Config Config;
        static List<TibiaVersion> InstalledVersions = new List<TibiaVersion>();

        static void Main(string[] args)
        {
            //TODO: Support operations via the args array.
            DoConfigChecks();
            DetectTibiaVersions();
            PrintCommands();

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

        private static void DetectTibiaVersions()
        {
            PPStep("versions");

            foreach (TibiaVersion tibiaVersion in TibiaVersions.Versions)
            {
                string path = Path.Combine(Config.Path, tibiaVersion.Shortname);

                if (Directory.Exists(path))
                {
                    PPFormat($"Found version {tibiaVersion.Fullname}...");
                    InstalledVersions.Add(tibiaVersion);
                }

                bool mirrorExists = CheckMirror(tibiaVersion.Shortname);

                if (mirrorExists)
                {
                    PPFormat($"Mirror {tibiaVersion.Fullname}.zip [{tibiaVersion.Shortname}]: OK");
                }
                else
                {
                    PPFormat($"Mirror {tibiaVersion.Fullname}.zip [{tibiaVersion.Shortname}]: MISSING");
                }
            }
        }

        private static bool CheckMirror(string shortname)
        {
            try
            {
                string url = $"https://github.com/thebma/tibiaexr/blob/master/mirror/{shortname}.zip";

                HttpWebRequest webReq = WebRequest.Create(url) as HttpWebRequest;
                webReq.Method = "HEAD";

                HttpWebResponse webResp = webReq.GetResponse() as HttpWebResponse;
                HttpStatusCode code = webResp.StatusCode;

                webResp.Close();

                if (code == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } 
            catch
            {
                return false;
            }

        }

        private static void PrintCommands()
        {
            //TODO: Instead this, could we store a synopis and elaborate usage of commands inside the command objects?
            //      This way we don't need to hardcode it and give the user a "explain" command for detailed usage.
            PPClear();
            PPStep("commands");
            PPFormat("General commands:");
            PPFormat("\treset - Resets the config and lets you reconfigure the application.");
            PPFormat("\tfetch <shortcode> - Download and install a  tibia version");
            PPFormat("Sprite commands:");
            PPFormat("\tsprunpack <version> <composite> <png|bmp> - Unpack sprites.");
            PPFormat("\tsprpack <version> <png|bmp> - Pack sprites.");
            PPFormat("\tsprcomp <version> <png|bmp> - Compare and output sprites.");
            PPFormat("\tsprsheet <version> <png|bmp> - Create a spritesheet.");
            PPFormat("\tsprsig <version|all> - Scan a specific or all sprites for their signatures.");
        }
    }
}
