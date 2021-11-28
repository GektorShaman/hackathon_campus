using hackathon_campus.Core.DataAccess;
using hackathon_campus.Core.Entities;
using hackathon_campus.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.Services
{
    public class EventService
    {
        private readonly IEventRepository _eventRepository;

        private readonly ICategoryRepository _categoryRepository;

        private readonly ImageService _imageService;

        private readonly MailService _mailService;

        private readonly UserService _userService;

        public EventService(IEventRepository eventRepository,ICategoryRepository categoryRepository,
            ImageService imageService, MailService mailService, UserService userService)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _imageService = imageService;
            _mailService = mailService;
            _userService = userService;
        }

        public async Task CreateEvent(CreateEventViewModel createEventViewModel)
        {
            var user = _userService.GetCurrentUser();
            var model = new Event()
            {
                Title = createEventViewModel.Title,
                Description = createEventViewModel.Description,
                MeetingPoint = createEventViewModel.MeetingPoint,
                Category = _categoryRepository.GetCategoryById(createEventViewModel.CategoryId),
                EventDateStart = createEventViewModel.EventDateStart,
                EventDateEnd = createEventViewModel.EventDateEnd,
                ApplicationUserId = user.Id,
                Image = new Image
                {
                    Path = _imageService.AddImage(createEventViewModel.Image)
                }
            };
            _eventRepository.CreateEvent(model);
            await _mailService.SendEmail(user.Email, createEventViewModel.Title, 
                createEventViewModel.Description);
        }

        public void DeleteEvent(Guid id)
        {
            _eventRepository.DeleteEvent(id);
        }

        public IEnumerable<EventViewModel> GetEvents(int page)
        {
            return _eventRepository.GetEvents(page,pageSize:20).Select(x => new EventViewModel
            {
                Id = x.Id,
                Title = x.Title,
                MeetingPoint = x.MeetingPoint,
                EventDateStart = x.EventDateStart,
                EventDateEnd = x.EventDateEnd,
                CategoryName = x.Category.Name,
                Organizer = x.ApplicationUser.FirstName + x.ApplicationUser.LastName,
                ImagePath = x.Image.Path
            });
        }

        public EventDetailsViewModel GetSinglEvent(Guid id)
        {
            var model = _eventRepository.GetSinglEvent(id);
            return new EventDetailsViewModel
            {
                Title = model.Title,
                Description = model.Description,
                MeetingPoint = model.MeetingPoint,
                EventDateStart = model.EventDateStart,
                EventDateEnd = model.EventDateEnd,
                CategoryName = model.Category.Name,
                Organizer = model.ApplicationUser.FirstName + model.ApplicationUser.LastName,
                ImagePath = model.Image.Path
            };
        }

        public IEnumerable<EventViewModel> GetEventsByCategory(Guid categoryId,int page)
        {
            return _eventRepository.GetEventsByCategory(categoryId,page, pageSize: 20)
                .Select(x => new EventViewModel
            {
                Id = x.Id,
                Title = x.Title,
                MeetingPoint = x.MeetingPoint,
                EventDateStart = x.EventDateStart,
                EventDateEnd = x.EventDateEnd,
                CategoryName = x.Category.Name,
                Organizer = x.ApplicationUser.FirstName + x.ApplicationUser.LastName,
                ImagePath = x.Image.Path
            });
        }
    }
}
