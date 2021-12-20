using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem.Terminals;

namespace Task3.EventsArgs
{
    public class StationStartCallAfterAnswerEventArgs : EventArgs
    {
        public StationStartCallAfterAnswerEventArgs(Guid sourceTerminalIdTerminal, Guid targetTerminalIdTerminal, bool isAccept)
        {
            SourceTerminalId = sourceTerminalIdTerminal;
            TargetTerminalId = targetTerminalIdTerminal;
            IsAccept = isAccept;
        }


        public Guid SourceTerminalId { get; set; }

        public Guid TargetTerminalId { get; set; }

        public bool IsAccept { get; set; }
    }
}
