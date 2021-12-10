using System.Linq;
using Task2.Core.Model;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Tasks.Commands
{
    internal class DeleteWordsByLengthCommand
    {
        private readonly IText _text;

        public DeleteWordsByLengthCommand(IText text)
        {
            _text = text;
        }

        public void Execute(int wordLength)
        {
            var q = _text
                .SelectMany(s => s.OfType<Word>())
                .Where(w => w.Length() == wordLength && Consonant.IsConsonantChar(w.FirstOrDefault()?.ToString()?[0]))
                .ToList();


            for (int i = 0; i < _text.Count(); i++)
            {
                for(int j = 0; j < _text.ElementAt(i).Count(); j++)
                {
                    if (q.Contains(_text.ElementAt(i).ElementAt(j)))
                    {
                        _text.ElementAt(i).RemoveAt(j);
                    }
                }
            }
        }
    }
}
