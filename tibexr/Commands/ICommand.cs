using System.Collections.Generic;

namespace tibexr.Commands
{
    public interface ICommand
    {
        public string GetSynopsis();
        public string GetDetailed();

        public void Execute(List<string> args);
    }
}
