using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.Entities
{
    public class CategorySubscription
    {
        public string ApplicationUserId { get; set; }

        public Guid CategoryId { get; set; }
    }
}
