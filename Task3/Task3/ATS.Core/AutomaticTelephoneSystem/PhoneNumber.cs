namespace ATS.Core.AutomaticTelephoneSystem
{
    public class PhoneNumber
    {
        public string Number { get; }

        public PhoneNumber(string number)
        {
            Number = number;
        }

        public static PhoneNumber Empty => new ("+000000000000");

        public override string ToString()
        {
            return $"{Number[..4]}({Number[4..6]}){Number[6..9]}-{Number[9..11]}-{Number[11..13]}";
        }


    }
}
