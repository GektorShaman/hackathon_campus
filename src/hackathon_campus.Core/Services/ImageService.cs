using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.Services
{
    public class ImageService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;

        public ImageService(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public string AddImage(IFormFile image)
        {
            var localPath = Path.Combine(_configuration.GetSection("UploadedImagesPath").Value, Path.GetRandomFileName());
            localPath = Path.ChangeExtension(localPath, "png");
            var filePath = Path.Combine(_environment.ContentRootPath + "\\wwwroot", localPath);
            using (var stream = File.Create(filePath))
            {
                image.CopyTo(stream);
            }
            return localPath;
        }
    }

}
