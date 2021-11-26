using hackathon_campus.Core.DataAccess;
using hackathon_campus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Infrastructure.DataAccess
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateEvent(Event newEvent)
        {
            _context.Add(newEvent);
            _context.SaveChanges();
        }

        public void DeleteEvent(Guid id)
        {
            _context.Events.Remove(_context.Events.FirstOrDefault(@event => @event.Id == id));
            _context.SaveChanges();
        }

        public IEnumerable<Event> GetEvents()
        {
            return _context.Events
                .ToList();
        }

        public IEnumerable<Event> GetEventsByCategory()
        {
            throw new NotImplementedException();
        }

        public Event GetSinglEvent(Guid id)
        {
            return _context.Events
                .FirstOrDefault(@event => @event.Id == id);
        }
    }
}
