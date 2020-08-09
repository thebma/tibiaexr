using System;
using System.IO;
using System.Net;
using System.Collections.Generic;

using tibexr.Util;
using tibexr.Static;
using tibexr.Commands;

using static tibexr.Util.PrettyPrint;

namespace tibexr
{
    class Program
    {
        static int CONFIG_VERSION = 1; //Any alterations to the config stucture should be paired with incrementing this number.

        static Config Config;
        static List<TibiaVersion> InstalledVersions = new List<TibiaVersion>();
        static Dictionary<string, ICommand> Commands = new Dictionary<string, ICommand>
        {
            { "fetch", new FetchCommand() }
        };

        static void Main(string[] args)
        {
            //TODO: Support operations via the args array.
            DoConfigChecks();
            DetectTibiaVersions();
            PrintCommands();
            ParseCommands();

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
            }
        }

        private static void PrintCommands()
        {
            //TODO: Instead of this, could we store a synopis and elaborate usage of commands inside the command objects?
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

        private static void ParseCommands()
        {
            while (true)
            {
                //Print the commands again, ask for user input.
                PrintCommands();
                PPFormat("");
                PPFormatInline("Enter a command: ");

                string commandInput = Console.ReadLine();
                if (string.IsNullOrEmpty(commandInput))
                {
                    PPFormat("You did not enter a command.");
                    PPWait();
                    continue;
                }

                string[] commandText = commandInput.Split(' ');
                if (commandText.Length < 1)
                {
                    PPFormat("You did not enter a command.");
                    PPWait();
                    continue;
                }

                string command = null;
                List<string> args = new List<string>();

                //Extract the command and arguments.
                foreach (string commandStr in commandText)
                {
                    if (string.IsNullOrEmpty(commandStr))
                    {
                        continue;
                    }

                    if (command == null)
                    {
                        command = commandStr;
                    }
                    else
                    {
                        args.Add(commandStr);
                    }
                }

                // Try running the command.
                bool foundCommand = Commands.TryGetValue(command, out ICommand commandObj);

                if (!foundCommand)
                {
                    PPFormat($"The specified command '{command}' was not found");
                    PPWait();
                    continue;
                }
                else
                {
                    PPFormat($"Running command '{command}' with args [{string.Join(',', args)}]...");

                    bool didCommandRunSuccessfully = commandObj.Execute(args);
                    PPFormat(didCommandRunSuccessfully ? "Ran command successfully." : "An error occured when running this command.");

                    PPWait();
                }
            }

        }
    }
}
