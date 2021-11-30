﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.TextObjectModel.Symbols.OneSign
{
    public class Comma : Symbol, ISymbol
    {
        private const char COMMA_CHAR = ',';

        public Comma()
            : base(COMMA_CHAR, SymbolType.PunctuationMark)
        {

        }

        //public string Writing => COMMA_CHAR.ToString();

        public override string ToString() => _symbol.ToString();
    }
}
