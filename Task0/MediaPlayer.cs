using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task0
{
    public class MediaPlayer
    {
        public MediaPlaylist CurrentPlaylist { get; set; }

        private SearchClass _searchClass;

        public void Play()
        {

        }

        public void Find()
        {
            _searchClass = new SearchClass(CurrentPlaylist);
            _searchClass.Find();
        }


    }
}