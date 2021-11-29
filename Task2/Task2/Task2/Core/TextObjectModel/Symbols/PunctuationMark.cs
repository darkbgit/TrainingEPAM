using Task2.Core.TextObjectModel.Interfaces;

namespace Task2.Core.TextObjectModel
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
