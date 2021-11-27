using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace hackathon_campus.Core.ViewModels
{
    public class CreateEventViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string MeetingPoint { get; set; }

        public DateTime EventDateStart { get; set; }

        public DateTime EventDateEnd { get; set; }

        public string CategoryName { get; set; }

        public Guid CategoryId { get; set; }

        public ICollection<string> Tags { get; set; }

        public string UserId { get; set; }

        public IFormFile Image { get; set; }
    }
}
