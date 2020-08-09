using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using tibexr.Util;
using tibexr.Static;
using tibexr.Network;

namespace tibexr.Commands
{
    public class FetchCommand : ICommand
    {
        public bool Execute(List<string> args)
        {
            if (args == null || args.Count < 1)
            {
                return false;
            }

            string shortCode = args[0];

            if (!TibiaVersions.IsVersionSupported(shortCode))
            {
                PrettyPrint.PPFormat($"Version {shortCode} is not a valid tibia version.");
                return false;
            }

            ClientDownloadMirror.FetchDistro(shortCode);
            return true;
        }

        public string GetUsage()
        {
            throw new NotImplementedException();
        }

        public string GetSynopsis()
        {
            throw new NotImplementedException();
        }
    }
}
