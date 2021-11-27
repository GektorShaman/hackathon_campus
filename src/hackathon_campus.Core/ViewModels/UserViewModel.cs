using hackathon_campus.Core.Entities;
using System.Collections.Generic;

namespace hackathon_campus.Core.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string AvatarPath { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}