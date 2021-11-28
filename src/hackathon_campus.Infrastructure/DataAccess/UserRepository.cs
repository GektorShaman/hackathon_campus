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

        public ICollection<string> GetAllRoles() => _context.Roles.Select(r => r.Name).ToList();

        public ApplicationUser GetUserById(string id)
        {
            return _context.Users.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool IsSubscribeOnCategory(string userId, Guid categoryId)
        {
            return _context.CategorySubscriptions.Any(subscription =>
                subscription.ApplicationUserId == userId && subscription.CategoryId == categoryId);
        }

        public bool IsSubscribeOnEvent(string userId, Guid eventId)
        {
            return _context.EventSubscriptions.Any(subscription =>
                subscription.ApplicationUserId == userId && subscription.EventId == eventId);
        }

        public async Task UpdateUser(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public IEnumerable<CategorySubscription> GetCategorySubscribers(Guid id)
        {
            return _context.CategorySubscriptions
                .Where(subscription => subscription.CategoryId == id)
                .ToList();
        }

        public IEnumerable<EventSubscription> GetEventSubscriptionByUser(Guid userId)
        {
            return _context.EventSubscriptions
                .Where(subscription => subscription.ApplicationUserId == userId.ToString())
                .ToList();
        }

        public void AddTelegramInformation(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
