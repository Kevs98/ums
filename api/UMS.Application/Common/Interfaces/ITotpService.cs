using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Application.Common.Interfaces
{
    public interface ITotpService
    {
        string GenerateSecretKey();
        string GenerateOtpCode(string secretKey);
        bool ValidateOtpCode(string secretKey, int otpCode);
    }
}
