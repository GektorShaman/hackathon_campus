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

        public async Task DeleteUser(string id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public ApplicationUser GetUserById(string id)
        {
            return _context.Users.FirstOrDefault(a => a.Id == id) ?? throw new ArgumentNullException();
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        public async Task UpdateUser(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
