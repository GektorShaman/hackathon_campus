using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.Entities
{
    public class EventSubscription
    {
        public string ApplicationUserId { get; set; }

        public Guid EventId { get; set; }
    }
}
