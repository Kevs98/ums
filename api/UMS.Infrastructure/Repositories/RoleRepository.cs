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
    public class RoleRepository : IRoleRepository
    {
        private readonly UmsDbContext _context;

        public RoleRepository(UmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
