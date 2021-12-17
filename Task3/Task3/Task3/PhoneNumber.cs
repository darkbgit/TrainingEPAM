using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class PhoneNumber
    {
        public string Number { get; set; }

        public PhoneNumber(string number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return $"{Number[..4]}({Number[4..6]}){Number[6..9]}-{Number[9..11]}-{Number[11..13]}";
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
