using hackathon_campus.Core.DataAccess;
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
        private readonly IImageRepository _imageRepository;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;

        public ImageService(IImageRepository imageRepository, IConfiguration configuration, IHostEnvironment environment)
        {
            _imageRepository = imageRepository;
            _configuration = configuration;
            _environment = environment;
        }

        public async Task AddEventImage(IFormFile image, Guid eventId)
        {
            var localPath = Path.Combine(_configuration.GetSection("UploadedImagesPath").Value, Path.GetRandomFileName());
            localPath = Path.ChangeExtension(localPath, "png");
            var filePath = Path.Combine(_environment.ContentRootPath + "\\wwwroot", localPath);
            using (var stream = File.Create(filePath))
            {
                await image.CopyToAsync(stream);
            }
            await _imageRepository.AddEventImage(localPath, eventId);
        }

        public async Task AddEventDefaultImage(Guid eventId)
        {
            var localPath = Path.Combine(_configuration.GetSection("UploadedImagesPath").Value, "DefaultEventImage.png");
            await _imageRepository.AddEventImage(localPath, eventId);
        }


        public async Task<Guid> CreateImage(IFormFile image)
        {
            var localPath = Path.Combine(_configuration.GetSection("UploadedImagesPath").Value, Path.GetRandomFileName());
            localPath = Path.ChangeExtension(localPath, "png");
            var filePath = Path.Combine(_environment.ContentRootPath + "\\wwwroot", localPath);
            using (var stream = File.Create(filePath))
            {
                await image.CopyToAsync(stream);
            }
            return await _imageRepository.AddImage(localPath);
        }
        public async Task AddCategoryImage(IFormFile image, Guid categoryId)
        {
            var imagePath = Path.Combine(_configuration.GetSection("UploadedImagesPath").Value, Path.GetRandomFileName());
            var imageLocalPath = Path.ChangeExtension(imagePath, "svg");
            var imageFilePath = Path.Combine(_environment.ContentRootPath + "\\wwwroot", imageLocalPath);
            using (var stream = File.Create(imageFilePath))
            {
                await image.CopyToAsync(stream);
            }
            await _imageRepository.AddCategoryImage(imageLocalPath, categoryId);
        }

        public async Task AddCategoryDefaultImage(Guid categoryId)
        {
            var localPath = Path.Combine(_configuration.GetSection("UploadedImagesPath").Value, "DefaultEventImage.png");
            await _imageRepository.AddCategoryImage(localPath, categoryId);
        }
    }

}
