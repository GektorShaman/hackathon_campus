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

        private readonly UserService _userService;

        public EventController(EventService eventService, CategoryService categoryService
            ,UserService userService)
        {
            _eventService = eventService;
            _categoryService = categoryService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Details(Guid eventId)
        {
            var eventViewModel = _eventService.GetSinglEvent(eventId);
            eventViewModel.IsSubscribe = _userService.IsSubscribeOnEvent(eventId);
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

        [HttpGet]
        public IActionResult SubscribeOnEvent(Guid eventId)
        {
            _userService.SubscribeOnEvent(eventId);
            return RedirectToAction("Details", eventId);
        }

        [HttpGet]
        public IActionResult UnSubscribeOnEvent(Guid eventId)
        {
            _userService.UnSubscribeOnEvent(eventId);
            return RedirectToAction("Details", eventId);
        }

    }
}
