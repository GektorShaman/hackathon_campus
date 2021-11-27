using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace hackathon_campus.Core.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Event> Events { get; set; }

        public Image Avatar { get; set; }

        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
