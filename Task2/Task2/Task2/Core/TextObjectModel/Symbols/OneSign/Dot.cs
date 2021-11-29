﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.TextObjectModel.Interfaces;

namespace Task2.Core.TextObjectModel.Symbols.OneSign
{
    public class Dot : ISymbol, ISentenceElement
    {
        private const char DOT_CHAR = '.';

        public string Writing => DOT_CHAR.ToString();
    }
}
