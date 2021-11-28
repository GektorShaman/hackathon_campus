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

        private readonly ImageService _imageService;

        public CategoryService(ICategoryRepository categoryRepository,ImageService imageService)
        {
            _categoryRepository = categoryRepository;
            _imageService = imageService;
        }

        public IEnumerable<CategoryViewModel> GetCategories()
        {
            return _categoryRepository.GetAllCategories()
                .Select(category => new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                //ImagePath = category.Image.Image.Path
            });
        }

        public CategoryViewModel GetCategoryById(Guid id)
        {
            var model = _categoryRepository.GetCategoryById(id);
            return new CategoryViewModel
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                //ImagePath = model.Image.Image.Path
            };
        }

        public async Task CreateCategory(CreateCategoryViewModel categoryViewModel)
        {
            var model = new Category()
            {
                Name = categoryViewModel.Name,
                Description = categoryViewModel.Description,
            };
            _categoryRepository.CreateCategory(model);
            if (categoryViewModel.Image != null)
            {
                await _imageService.AddCategoryImage(categoryViewModel.Image, model.Id);
            }
            else
            {
                await _imageService.AddCategoryDefaultImage(model.Id);
            }
        }

        public void DeleteCategory(string name)
        {
            _categoryRepository.DeleteCategory(name);
        }

    }
}
