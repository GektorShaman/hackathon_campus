﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.Entities
{
    public class Event
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string MeetingPoint { get; set; }

        public int NumberParticipants { get; set; }

        public DateTime EventDateStart { get; set; }

        public DateTime EventDateEnd { get; set; }

        public EventImage Image { get; set; }

        public Category Category { get; set; }

        public Guid CategoryId { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
