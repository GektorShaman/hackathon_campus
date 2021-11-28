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


        private readonly ImageService _imageService;

        private readonly MailService _mailService;

        private readonly UserService _userService;

        private readonly CategoryService _categoryService;

        public EventService(IEventRepository eventRepository,
            ImageService imageService, MailService mailService, UserService userService
            ,CategoryService categoryService)
        {
            _eventRepository = eventRepository;
            _imageService = imageService;
            _mailService = mailService;
            _userService = userService;
            _categoryService = categoryService;
        }

        public async Task CreateEvent(CreateEventViewModel createEventViewModel)
        {
            var user = _userService.GetCurrentUser();
            var model = new Event()
            {
                Title = createEventViewModel.Title,
                Description = createEventViewModel.Description,
                MeetingPoint = createEventViewModel.MeetingPoint,
                CategoryId = createEventViewModel.CategoryId,
                EventDateStart = createEventViewModel.EventDateStart,
                EventDateEnd = createEventViewModel.EventDateEnd,
                ApplicationUserId = user.Id
            };
            _eventRepository.CreateEvent(model);
            if (createEventViewModel.Image != null)
            {
                await _imageService.AddEventImage(createEventViewModel.Image, model.Id);
            }
            else
            {
                await _imageService.AddEventDefaultImage(model.Id);
            }
            foreach (var item in _userService.GetCategorySubscribers(createEventViewModel.CategoryId))
            {
                await _mailService.SendEmail(item.Email, createEventViewModel.Title,
               createEventViewModel.Description);
            }
        }

        public void DeleteEvent(Guid id)
        {
            _eventRepository.DeleteEvent(id);
        }

        public IEnumerable<EventViewModel> GetEvents(int page)
        { 
            return _eventRepository.GetEvents(page, pageSize: 20).Select(x => new EventViewModel
            {
                Id = x.Id,
                Title = x.Title,
                MeetingPoint = x.MeetingPoint,
                EventDateStart = x.EventDateStart,
                EventDateEnd = x.EventDateEnd,
                CategoryName = x.Category.Name,
                Organizer = x.ApplicationUser.FirstName + x.ApplicationUser.LastName,
                ImagePath = x.Image.Image.Path
            });
        }

        public EventDetailsViewModel GetSinglEvent(Guid id)
        {
            var model = _eventRepository.GetSinglEvent(id);
            var eventDetailViewModel = new EventDetailsViewModel
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                //MeetingPoint = model.MeetingPoint,
                EventDateStart = model.EventDateStart,
                EventDateEnd = model.EventDateEnd,
                CategoryName = model.Category.Name,
                Organizer = model.ApplicationUser.FirstName + model.ApplicationUser.LastName,
                ImagePath = model.Image.Image.Path,
                NumberParticipants = model.NumberParticipants
            };
            if (model.ApplicationUserId == _userService.GetCurrentUser().Id)
            {
                eventDetailViewModel.isOwn = true;
            }
            else
            {
                eventDetailViewModel.isOwn = false;
            }
            return eventDetailViewModel;
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
                ImagePath = x.Image.Image.Path
            });
        }
    }
}
