using System;
using ATS.Core.AutomaticTelephoneSystem;

namespace ATS.Core.EventsArgs
{
    public class StationStartCallAnswerEventArgs : EventArgs
    {
        public StationStartCallAnswerEventArgs(bool isAccept, PhoneNumber called)
        {
            IsAccept = isAccept;
            Called = called;
        }

        public bool IsAccept { get; set; }

        public PhoneNumber Called { get; set; }

    }
}
