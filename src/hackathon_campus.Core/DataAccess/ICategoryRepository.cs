using hackathon_campus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.DataAccess
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetAllCategories();

        public Category GetCategoryByName(string name);

        public void CreateCategory(Category category);

        public void DeleteCategory(string name);
    }
}
