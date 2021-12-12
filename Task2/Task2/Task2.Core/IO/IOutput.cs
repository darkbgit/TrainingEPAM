using Task2.Core.Model.Interfaces;

namespace Task2.Core.IO
{
    public interface IOutput
    {
        void Print(string str);

        void Print(IText text);

        void Print(ISentence sentence);
    }
}
