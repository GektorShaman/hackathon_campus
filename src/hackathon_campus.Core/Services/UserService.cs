using hackathon_campus.Core.DataAccess;
using hackathon_campus.Core.Entities;
using hackathon_campus.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository
                            , UserManager<ApplicationUser> userManager
                            , IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public ApplicationUser GetCurrentUser()
        {
            var thisUser = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
            return thisUser;
        }

        public UserViewModel GetUserById(string id)
        {
            var user = _userRepository.GetUserById(id);
            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName =  user.LastName,
                NickName = user.UserName,
                Email = user.Email
            };
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            var users = _userRepository.GetUsers();
            return users.Select(user => new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NickName = user.UserName
            });
        }

        public async Task UpdateUser(UserViewModel model)
        {
            var user = _userRepository.GetUserById(model.Id);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.NickName;
            user.NormalizedUserName = model.NickName.ToUpper();
            user.Email = model.Email;
            user.NormalizedEmail = model.Email.ToUpper();
            await _userRepository.UpdateUser(user);
        }

        public async Task DeleteUser(string id)
        {
            await _userRepository.DeleteUser(id);
        }

        public void SubscribeOnCategory(Guid categoryId)
        {
            var subscription = new CategorySubscription
            {
                ApplicationUserId = GetCurrentUser().Id,
                CategoryId = categoryId
            };
            _userRepository.CategorySubscribe(subscription);
        }

        public void UnSubscribeOnCategory(Guid categoryId)
        {
            var subscription = new CategorySubscription
            {
                ApplicationUserId = GetCurrentUser().Id,
                CategoryId = categoryId
            };
            _userRepository.CategoryUnSubscribe(subscription);
        }

        public bool IsSubscribeOnCategory(Guid categoryId)
        {
            var user = GetCurrentUser();
            if (user != null)
            {
                return _userRepository.IsSubscribeOnCategory(user.Id, categoryId);
            }
            return false;
        }


        public void SubscribeOnEvent(Guid eventId)
        {
            var user = GetCurrentUser();
            var subscription = new EventSubscription
            {
                ApplicationUserId = GetCurrentUser().Id,
                EventId = eventId
            };
            _userRepository.EventSubscribe(subscription);
        }

        public void UnSubscribeOnEvent(Guid eventId)
        {
            var subscription = new EventSubscription
            {
                ApplicationUserId = GetCurrentUser().Id,
                EventId = eventId
            };
            _userRepository.EventUnSubscribe(subscription);
        }

        public bool IsSubscribeOnEvent(Guid eventId)
        {
            var user = GetCurrentUser();
            if (user != null)
            {
                return _userRepository.IsSubscribeOnCategory(user.Id, eventId);
            }
            return false;
        }
    }
}
