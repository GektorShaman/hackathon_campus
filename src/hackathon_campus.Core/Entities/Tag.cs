using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }

    }
}
