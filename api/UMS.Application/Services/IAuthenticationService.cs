using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Application.Services
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task<string> GenerateJwtAsync(string username);
    }
}
