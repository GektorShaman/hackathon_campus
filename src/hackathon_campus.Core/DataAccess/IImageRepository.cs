using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.DataAccess
{
    public interface IImageRepository
    {
        public Task AddEventImage(string path, Guid eventId);

        public Task AddCategoryImage(string path, Guid categoryId);

        public Task<Guid> AddImage(string imagePath);
    }
}
