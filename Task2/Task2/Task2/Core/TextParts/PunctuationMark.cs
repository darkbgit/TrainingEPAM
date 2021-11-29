using Task2.Core.TextParts.Interfaces;

namespace Task2.Core.TextParts
{
    public class PunctuationMark : ISentenceElement
    {
        public PunctuationMark(char writing)
        {
            Writing = writing.ToString();
        }
        public string Writing { get; set; }
        //public int Position { get; set; }
    }
}
