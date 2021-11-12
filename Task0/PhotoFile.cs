using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task0
{
    public class PhotoFile : MediaFile, IMedia
    {
        public PhotoFile(string filePath) : base(filePath)
        {
        }

        public void Play()
        {
            throw new NotImplementedException();
        }
    }
}