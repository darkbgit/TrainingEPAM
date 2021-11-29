using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Task2.Core.TextObjectModel;
using Task2.Core.TextObjectModel.Interfaces;


namespace Task2.Core.Analyzer
{
    public class TextAnalyzer : IAnalyzer
    {
        private readonly char[] SentencesSeparators = {'.', '!', '?', '\n'};

        private readonly char[] WordsSeparators = {' ', '\t'};
        public IText Analyze(string text)
        {

            char[] SentencesSeparator = {'.', '!', '?'};

            //var sentences = text
            //    .Split(SentencesSeparator, StringSplitOptions.TrimEntries)
            //    .Where(s => !string.IsNullOrWhiteSpace(s))
            //    .ToList();

            var sentences = Regex.Split(text, @"(?<=[\.!\?\r\n])\s+");
            var result = new List<ISentence>();
            for (var index = 0; index < sentences.Length; index++)
            {
                var sentence = sentences[index];
                result.Add(AnalyzeSentence(sentence));
            }

            return new Text(result);

        }

        private ISentence AnalyzeSentence(string sentence)
        {
            var words = sentence.Split(WordsSeparators, StringSplitOptions.TrimEntries)
                .Where(s => !string.IsNullOrWhiteSpace(s));
            List<ISentenceElement> elements = new();
            int i = 0;

            foreach (var word in words)
            {
                elements.AddRange(AnalyzeWord(word, sentence, i++));
                
                
            }
            return new Sentence(elements);
        }

        private IEnumerable<ISentenceElement> AnalyzeWord(string word, string sentence, int i)
        {
            //IEnumerable<char> punctuationsMarks = new List<char> {'.', ',', ':'};

            //List<ISentenceElement> elements = new();

            //if (punctuationsMarks.Contains(word.Last()))
            //{
            //    elements.Add(new Word(word[..^1]));
            //    elements.Add(new PunctuationMark(word.Last()));
            //}
            //else
            //{
            //    elements.Add(new Word(word));
            //}

            return default;
        }
    }
}
