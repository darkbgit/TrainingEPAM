using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task0
{
    public class VideoFile : MediaFile, IMedia
    {
        public VideoFile(string filePath) : base(filePath)
        {
        }

        public void Play()
        {
            throw new NotImplementedException();
        }
    }
}