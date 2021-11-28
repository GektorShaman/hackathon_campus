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

            var models = _categoryRepository.GetAllCategories();
            var categoryViewModel = new List<CategoryViewModel>();
            foreach (var item in models)
            {
                var model = new CategoryViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    //ImagePath = category.Image.Image.Path
                };
                foreach (var innerItem in item.Events)
                {
                    model.NumberParticipants += innerItem.NumberParticipants;
                }
                categoryViewModel.Add(model);
            }
            return categoryViewModel;
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
