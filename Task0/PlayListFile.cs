using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task0
{
    public class PlayListFile : MediaFile , IMedia
    {
        public PlayListFile(string filePath) : base(filePath)
        {
        }

        public MediaFilePlaylist Files { get; set; }

        public void Play()
        {
            throw new NotImplementedException();
        }


    }
}