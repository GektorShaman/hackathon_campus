using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.ViewModels
{
    public class CategoryEventViewModel
    {
        public IEnumerable<EventViewModel> EventViewModels { get; set; }

        public Guid Id { get; set; }

        public bool IsSubscribe { get; set; }
        
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
    }
}
