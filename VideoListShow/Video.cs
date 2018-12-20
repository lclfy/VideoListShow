using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VideoListShow
{
    class Video
    {
        public FileInfo fileInfo{get;set;}
        public string thumbPath { get; set; }

        public Video(FileInfo _f)
        {
            fileInfo = _f;
            thumbPath = "";
        }
    }
}
