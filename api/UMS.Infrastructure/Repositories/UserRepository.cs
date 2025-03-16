using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UMS.Core.Entities;
using UMS.Core.Interfaces;
using UMS.Infrastructure.Persistence;

namespace UMS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UmsDbContext _context;

        public UserRepository(UmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByUsername(string username, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(k => k.username == username && k.password == password);
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            var userDb = await GetByUsername(user.username, user.password);
            if (userDb != null)
            {
                foreach(var property in typeof(User).GetProperties())
                {
                    var newValue = property.GetValue(user);
                    if (newValue != null)
                    {
                        var entityProperty = typeof(User).GetProperty(property.Name);
                        entityProperty?.SetValue(userDb, newValue);
                    }
                }
                _context.Entry(userDb).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
