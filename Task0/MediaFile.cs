using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task0
{
    public abstract class MediaFile
    {
        protected MediaFile(string filePath)
        {
            Id = Guid.NewGuid();
            FilePath = filePath;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string FilePath { get; set; }
    }
}