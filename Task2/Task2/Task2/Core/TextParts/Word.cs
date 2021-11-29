using Task2.Core.TextParts.Interfaces;

namespace Task2.Core.TextParts
{
    public class Word : ISentenceElement
    {

        public Word(string writing)
        {
            Writing = writing;
        }

        public string Writing { get; }

        //public int Position { get; set; }



    }
}
