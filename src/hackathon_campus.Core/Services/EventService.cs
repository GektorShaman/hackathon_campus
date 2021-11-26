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

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        void CreateEvent(EventViewModel eventViewModel)
        {
            var model = new Event()
            {
                Title = eventViewModel.Title,
                Description = eventViewModel.Description,
                MeetingPoint = eventViewModel.MeetingPoint
            };
            _eventRepository.CreateEvent(model);
        }
        void DeleteEvent(Guid id)
        {
            _eventRepository.DeleteEvent(id);
        }

        IEnumerable<EventViewModel> GetEvents()
        {
            return _eventRepository.GetEvents().Select(x => new EventViewModel
            {
                Title = x.Title,
                Description = x.Description,
                MeetingPoint = x.MeetingPoint,
                Id = x.Id
            });
        }

        EventViewModel GetSinglEvent(Guid id)
        {
            var model = _eventRepository.GetSinglEvent(id);
            return new EventViewModel
            {
                Title = model.Title,
                Description = model.Description,
                MeetingPoint = model.MeetingPoint,
                Id = model.Id
            };
        }
    }
}
