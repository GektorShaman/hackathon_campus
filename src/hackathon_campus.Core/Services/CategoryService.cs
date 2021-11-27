using hackathon_campus.Core.DataAccess;
using hackathon_campus.Core.Entities;
using hackathon_campus.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryViewModel> GetCategories()
        {
            return _categoryRepository.GetAllCategories()
                .Select(category => new CategoryViewModel
            {
                Name = category.Name,
                Description = category.Description,
                //ImagePath = category.Image.Path
            });
        }

        public void CreateCategory(CategoryViewModel categoryViewModel)
        {
            var model = new Category()
            {
                Name = categoryViewModel.Name,
                Description = categoryViewModel.Description


            };

            _categoryRepository.CreateCategory(model);
        }
        public void DeleteCategory(string name)
        {
            _categoryRepository.DeleteCategory(name);
        }

    }
}
