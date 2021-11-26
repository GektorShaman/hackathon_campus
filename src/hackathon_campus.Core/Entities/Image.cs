using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.Entities
{
    public class Image
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
