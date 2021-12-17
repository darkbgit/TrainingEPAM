namespace Task3.EventsArgs
{
    public class AnswerCallEventArgs
    {
        public AnswerCallEventArgs(bool answerTheCall, PhoneNumber caller, PhoneNumber called)
        {
            AnswerTheCall = answerTheCall;
            Caller = caller;
            Called = called;
        }

        public bool AnswerTheCall { get; set; }

        public PhoneNumber Caller { get; set; }

        public  PhoneNumber Called { get; set; }
    }
}