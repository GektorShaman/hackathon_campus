using hackathon_campus.Core.DataAccess;
using hackathon_campus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Infrastructure.DataAccess
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(string name)
        {
            _context.Categories.Remove(
                _context.Categories.FirstOrDefault(category => category.Name == name));
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories
                .Include(category => category.Image)
                .ToList();
        }

        public Category GetCategoryByName(string name)
        {
            return _context.Categories
                .Include(category => category.Image)
                .Where(category => category.Name == name)
                .FirstOrDefault();
        }
    }
}
