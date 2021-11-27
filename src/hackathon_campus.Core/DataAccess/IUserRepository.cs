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
    }
}
