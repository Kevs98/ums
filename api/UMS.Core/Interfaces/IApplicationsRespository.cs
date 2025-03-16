using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Core.Entities;

namespace UMS.Core.Interfaces
{
    public interface IApplicationsRespository
    {
        Task<IEnumerable<Applications>> GetApplications();
        Task<Applications> GetApplication(int applicationId);
        Task AddApplication(Applications application);
        Task UpdateApplication(Applications application);
    }
}
