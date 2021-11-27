using hackathon_campus.Core.Services;
using hackathon_campus.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackathon_campus.Controllers
{
    //[Authorize]
    public class EventController : Controller
    {
        private readonly EventService _eventService;

        private readonly CategoryService _categoryService;

        public EventController(EventService eventService, CategoryService categoryService)
        {
            _eventService = eventService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Details(Guid eventId)
        {
            var eventViewModel = _eventService.GetSinglEvent(eventId);
            return View(eventViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Category"] = new SelectList(_categoryService.GetCategories(),"Name","Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create");
            }
            
            //add user service to get userId 
            
            _eventService.CreateEvent(model);

            return RedirectToAction("Index","Main");
        }


    }
}
