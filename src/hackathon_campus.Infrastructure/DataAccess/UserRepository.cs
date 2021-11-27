using hackathon_campus.Core.DataAccess;
using hackathon_campus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackathon_campus.Infrastructure.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CategorySubscribe(CategorySubscription categorySubscription)
        {
            _context.CategorySubscriptions.Add(categorySubscription);
            _context.SaveChanges();
        }

        public void CategoryUnSubscribe(CategorySubscription categorySubscription)
        {
            _context.CategorySubscriptions.Remove(categorySubscription);
            _context.SaveChanges();
        }

        public async Task DeleteUser(string id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public void EventSubscribe(EventSubscription eventSubscription)
        {
            _context.EventSubscriptions.Add(eventSubscription);
            _context.SaveChanges();
        }

        public void EventUnSubscribe(EventSubscription eventSubscription)
        {
            _context.EventSubscriptions.Remove(eventSubscription);
            _context.SaveChanges();
        }

        public ApplicationUser GetUserById(string id)
        {
            return _context.Users.FirstOrDefault(a => a.Id == id) ?? throw new ArgumentNullException();
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool IsSubscribeOnCategory(string userId, Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public bool IsSubscribeOnEvent(string userId, Guid eventId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUser(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


    }
}
