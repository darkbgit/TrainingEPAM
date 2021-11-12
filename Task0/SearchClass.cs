using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task0
{
    public class SearchClass
    {
        private readonly IEnumerable<IMedia> _medias;
        public SearchClass(IEnumerable<IMedia> medias)
        {
            _medias = medias;
        }

        public IEnumerable<IMedia> Find()
        {
            return default(IEnumerable<IMedia>);
        }
    }
}