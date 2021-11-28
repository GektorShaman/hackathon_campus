using hackathon_campus.Core.DataAccess;
using hackathon_campus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Infrastructure.DataAccess
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCategoryImage(string path, Guid categoryId)
        {
            var image = new Image { Path = path };
            _context.Add(image);
            _context.SaveChanges();
            var categoryImage = new CategoryImage
            {
                CategoryId = categoryId,
                ImageId = image.Id
            };
            await _context.AddAsync(categoryImage);
            await _context.SaveChangesAsync();
        }

        public async Task AddEventImage(string path, Guid eventId)
        {
            var image = new Image { Path = path };
            _context.Add(image);
            _context.SaveChanges();
            var eventImage = new EventImage
            {
                EventId = eventId,
                ImageId = image.Id
            };
            await _context.AddAsync(eventImage);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> AddImage(string imagePath)
        {
            var image = new Image { Path = imagePath };
            await _context.AddAsync(image);
            await _context.SaveChangesAsync();
            return image.Id;
        }
    }
}
