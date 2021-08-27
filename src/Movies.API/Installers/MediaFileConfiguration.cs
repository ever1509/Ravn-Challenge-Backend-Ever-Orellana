﻿using Microsoft.AspNetCore.Http;

namespace Movies.API.Installers
{
    public class MediaFileFormRequest
    {
        public IFormFile File { get; set; }
        public string Folder { get; set; }
        public int Id { get; set; }
    }
}
