using hackathon_campus.Core.Services;
using hackathon_campus.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackathon_campus.Controllers
{
    public class CategoryController : Controller
    {
        private readonly EventService _eventService;

        private readonly CategoryService _categoryService;

        private readonly UserService _userService;

        public CategoryController(EventService eventService, CategoryService categoryService
            , UserService userService)
        {
            _eventService = eventService;
            _categoryService = categoryService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categoriesViewModel = _categoryService.GetCategories();
            return View(categoriesViewModel);
        }

        [HttpGet]
        public ActionResult<CategoryEventViewModel> Details(Guid categoryId, int page)
        {
            var viewModel = new CategoryEventViewModel
            {
                EventViewModels = _eventService.GetEventsByCategory(categoryId,page),
                Id = categoryId,
                IsSubscribe = _userService.IsSubscribeOnCategory(categoryId),
                ImagePath = _categoryService.GetCategoryById(categoryId).ImagePath,
            };
            return View(viewModel);
        }
    }
}
