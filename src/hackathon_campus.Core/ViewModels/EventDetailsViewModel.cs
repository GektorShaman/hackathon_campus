using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.ViewModels
{
    public class EventDetailsViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string MeetingPoint { get; set; }

        public DateTime EventDateStart { get; set; }

        public DateTime EventDateEnd { get; set; }

        public ICollection<string> Tags { get; set; }

        public string CategoryName { get; set; }


        public string Organizer { get; set; }

        public string ImagePath { get; set; }

        public bool IsSubscribe { get; set; }

        public bool isOwn { get; set; }

        public int NumberParticipants { get; set; }

    }
}
