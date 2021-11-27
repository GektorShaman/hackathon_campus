﻿using hackathon_campus.Core.Entities;
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
        public void CreateEvent(Event newEvent);
        public void DeleteEvent(Guid id);
        public IEnumerable<Event> GetEvents(int pages, int pageSize);

        public IEnumerable<Event> GetEventsByCategory(string name, int pages, int pageSize);

        public Event GetSinglEvent(Guid id);
    }
}
