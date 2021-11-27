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
    public class EventService
    {
        private readonly IEventRepository _eventRepository;

        private readonly ICategoryRepository _categoryRepository;

        public EventService(IEventRepository eventRepository,ICategoryRepository categoryRepository)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
        }

        public void CreateEvent(CreateEventViewModel createEventViewModel)
        {
            var model = new Event()
            {
                Title = createEventViewModel.Title,
                Description = createEventViewModel.Description,
                MeetingPoint = createEventViewModel.MeetingPoint,
                Category = _categoryRepository.GetCategoryByName(createEventViewModel.CategoryName),
                EventDateStart = createEventViewModel.EventDateStart,
                EventDateEnd = createEventViewModel.EventDateEnd,
                ApplicationUserId = createEventViewModel.UserId  
            };

            _eventRepository.CreateEvent(model);
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

        public IEnumerable<EventViewModel> GetEventsByCategory(string categoryName,int page)
        {
            return _eventRepository.GetEventsByCategory(categoryName,page, pageSize: 20)
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
