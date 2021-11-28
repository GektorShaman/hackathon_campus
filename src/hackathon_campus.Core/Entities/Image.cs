using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.Entities
{
    public class Image
    {
        public Guid Id { get; set; }

        public string Path { get; set; }

        public ICollection<EventImage> EventImages { get; set; }

        public ICollection<CategoryImage> CategoryImages { get; set; }

        //public string ApplicationUserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }

    }
}
