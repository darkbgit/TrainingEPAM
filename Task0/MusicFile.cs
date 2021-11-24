using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task0
{
    public class MusicFile : MediaFile, IMedia
    {
        public MusicFile(string fileName) : base(fileName)
        {
            
        }

        public void Play()
        {
            throw new NotImplementedException();
        }
    }
}