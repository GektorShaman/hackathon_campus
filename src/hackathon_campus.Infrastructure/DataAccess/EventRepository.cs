using hackathon_campus.Core.DataAccess;
using hackathon_campus.Core.Entities;
using Microsoft.EntityFrameworkCore;
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

        public EventRepository(ApplicationDbContext context)
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
            _context.Events.Remove(
                _context.Events.FirstOrDefault(@event => @event.Id == id));
            _context.SaveChanges();
        }

        public IEnumerable<Event> GetEvents(int pages,int pageSize)
        {
            return _context.Events
                .Include(@event => @event.Image)
                .Include(@event => @event.Category)
                .Include(@event => @event.ApplicationUser)
                .Include(@event => @event.Tags)
                .Skip((pages - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .OrderBy(@event => @event.EventDateStart);
        }

        public IEnumerable<Event> GetEventsByCategory(Guid id, int pages, int pageSize)
        {
            return _context.Events
                .Include(@event => @event.Image)
                .Include(@event => @event.Category)
                .Include(@event => @event.ApplicationUser)
                .Include(@event => @event.Tags)
                .Where(@event => @event.Category.Id == id)
                .Skip((pages - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .OrderBy(@event => @event.EventDateStart);
        }

        public Event GetSinglEvent(Guid id)
        {
            return _context.Events
                .Include(@event => @event.Image)
                .Include(@event => @event.Category)
                .Include(@event => @event.ApplicationUser)
                .Include(@event => @event.Tags)
                .FirstOrDefault(@event => @event.Id == id);
        }
    }
}
