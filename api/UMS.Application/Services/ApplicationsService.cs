using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Core.Entities;
using UMS.Core.Interfaces;

namespace UMS.Application.Services
{
    public class ApplicationsService
    {
        private readonly IApplicationsRespository _applicationsRepository;
        private readonly IApplicationTypeRepository _applicationTypeRepository;

        public ApplicationsService(IApplicationsRespository applicationsRepository, IApplicationTypeRepository applicationTypeRepository)
        {
            _applicationsRepository = applicationsRepository;
            _applicationTypeRepository = applicationTypeRepository;
        }

        public async Task<IEnumerable<Applications>> GetApplications()
        {
            return await _applicationsRepository.GetApplications();
        }

        public async Task<Applications> GetApplication(int applicationId)
        {
            return await _applicationsRepository.GetApplication(applicationId);
        }

        public async Task AddApplication(Applications application)
        {
            await _applicationsRepository.AddApplication(application);
        }

        public async Task UpdateApplication(Applications application)
        {
            await _applicationsRepository.UpdateApplication(application);
        }

        public async Task<IEnumerable<ApplicationType>> GetApplicationTypes()
        {
            return await _applicationTypeRepository.GetApplicationTypes();
        }
    }
}
