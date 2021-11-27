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
    }
}
