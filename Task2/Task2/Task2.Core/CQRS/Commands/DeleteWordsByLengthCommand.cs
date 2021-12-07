using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Model;
using Task2.Core.Model.Interfaces;
using Task2.Core.Tasks;

namespace Task2.Core.CQRS.Commands
{
    internal class DeleteWordsByLengthCommand
    {
        private IText _text;

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
                for(int j = 0; j < _text.ElementAt(i).Count(); i++)
                {
                    if (q.Contains(_text.ElementAt(i).ElementAt(j)))
                    {
                        _text.ElementAt(i).RemoveAt(j);
                    }
                }
            }

            //var r = new Text(_text
            //    .Select(s => new Sentence(
            //        s.Where(se => !s
            //            .OfType<Word>()
            //            .Where(w => w.Length() == wordLength &&
            //                        Consonant.IsConsonantChar(w.FirstOrDefault()?.ToString()?[0]))
            //            .Select(e => e as ISentenceElement)
            //            .Contains(se)))));
        }
    }
}
