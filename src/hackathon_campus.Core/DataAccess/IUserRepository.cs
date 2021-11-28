using hackathon_campus.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.DataAccess
{
    public interface IUserRepository
    {
        ApplicationUser GetUserById(string id);
        IEnumerable<ApplicationUser> GetUsers();

        Task DeleteUser(string id);

        Task UpdateUser(ApplicationUser user);

        public ICollection<String> GetAllRoles();

        public void CategorySubscribe(CategorySubscription categorySubscription);

        public void CategoryUnSubscribe(CategorySubscription categorySubscription);

        public bool IsSubscribeOnCategory(string userId, Guid categoryId);

        public void EventSubscribe(EventSubscription eventSubscription);

        public void EventUnSubscribe(EventSubscription eventSubscription);

        public bool IsSubscribeOnEvent(string userId, Guid eventId);

        public IEnumerable<CategorySubscription> GetCategorySubscribers(Guid id);

        public IEnumerable<EventSubscription> GetEventSubscriptionByUser(Guid userId);

        public void AddTelegramInformation(ApplicationUser user);

    }
}
