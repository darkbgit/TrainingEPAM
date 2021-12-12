using System.Linq;

namespace Task2.Core.Tasks
{
    internal static class Consonant
    {
        private static readonly uint _russianConsonantMask;
        private static readonly uint _englishConsonantMask;
        private static readonly char[] _russianSortConsonant;
        private static readonly char[] _englishSortConsonant;

        private static readonly char[] _russianConsonant = {
            'Б', 'В', 'Г', 'Д', 'Ж', 'З', 'Й', 'К', 'Л', 'М', 'Н', 'П', 'Р', 'С', 'Т', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ',
            'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ'
        };

        private static readonly char[] _englishConsonant = {
            'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z',
            'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z'
        };


        static Consonant()
        {
            _russianSortConsonant = _russianConsonant.OrderBy(c => c).ToArray();
            _russianConsonantMask = SetConsonantMask(_russianConsonant, RussianShift);

            _englishSortConsonant = _englishConsonant.OrderBy(c => c).ToArray();
            _englishConsonantMask = SetConsonantMask(_englishConsonant, EnglishShift);
        }

        private static int RussianShift => _russianSortConsonant.First() - 1;

        private static int EnglishShift => _englishSortConsonant.First() - 1;

        private static uint SetConsonantMask(char[] consonant, int shift)
        {
            return consonant.Aggregate<char, uint>(0, (current, c1) => current | 1u << c1 - shift);
        }

        public static bool IsConsonantChar(char? c)
        {
            if (c == null)
            {
                return false;
            }

            if (c >= _russianSortConsonant.First() & c <= _russianSortConsonant.Last())
            {
                return (1u << c - RussianShift & _russianConsonantMask) != 0;
            }

            if (c >= _englishSortConsonant.First() & c <= _englishSortConsonant.Last())
            {
                return (1u << c - EnglishShift & _englishConsonantMask) != 0;
            }

            return false;
        }
    }
}
