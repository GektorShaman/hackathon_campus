using hackathon_campus.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackathon_campus.Controllers
{
    public class MainController : Controller
    {
        private readonly EventService _eventService;

        public MainController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var eventsViewModel = _eventService.GetEvents(page);
            return View(eventsViewModel);
        }
    }
}
