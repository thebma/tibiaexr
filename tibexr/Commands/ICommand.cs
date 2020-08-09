using System.Collections.Generic;

namespace tibexr.Commands
{
    public interface ICommand
    {
        public string GetSynopsis();
        public string GetUsage();

        public bool Execute(List<string> args);
    }
}
