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
    public class ApplicationsRepository : IApplicationsRespository
    {
        private readonly UmsDbContext _context;

        public ApplicationsRepository(UmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Applications>> GetApplications()
        {
            return await _context.Applications.ToListAsync();
        }

        public async Task<Applications> GetApplication(int applicationId)
        {
            return await _context.Applications.SingleOrDefaultAsync(k => k.id == applicationId);
        }

        public async Task AddApplication(Applications application)
        {
            await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateApplication(Applications application)
        {
            var applicationDb = await GetApplication((int)application.id);
            if (applicationDb != null)
            {
                foreach(var property in typeof(Applications).GetProperties())
                {
                    var newValue = property.GetValue(application);
                   if (newValue != null)
                    {
                        var entityProperty = typeof(Applications).GetProperty(property.Name);
                        entityProperty?.SetValue(applicationDb, newValue);
                    }
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
