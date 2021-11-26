using hackathon_campus.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.DataAccess
{
    public interface IEventRepository
    {
        void CreateEvent(Event newEvent);
        void DeleteEvent(Guid id);

        IEnumerable<Event> GetEvents();

        IEnumerable<Event> GetEventsByCategory();

        Event GetSinglEvent(Guid id);
    }
}
