using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Movies.Application.Common.Models.Requests
{
    public class MediaFileRequest
    {
        [JsonIgnore]
        public Stream FileData { get; set; }
        public string ContentType { get; set; }

        public string Folder { get; set; }

        private string _fileName;
        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(Folder)) return _fileName;
                return Path.Combine(Folder, _fileName).ToLower();
            }
            set
            {
                _fileName = value;
            }
        }
    }
}
