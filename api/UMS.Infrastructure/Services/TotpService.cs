using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OtpNet;
using UMS.Application.Common.Interfaces;

namespace UMS.Infrastructure.Services
{
    public class TotpService : ITotpService
    {
        public string GenerateSecretKey()
        {
            var key = KeyGeneration.GenerateRandomKey(20);
            return Base32Encoding.ToString(key);
        }

        public string GenerateOtpCode(string secretKey)
        {
            var keyBytes = Base32Encoding.ToBytes(secretKey);
            var totp = new Totp(keyBytes);
            return totp.ComputeTotp();
        }

        public bool ValidateOtpCode(string secretKey, int otpCode)
        {
            var keyBytes = Base32Encoding.ToBytes(secretKey);
            var totp = new Totp(keyBytes);
            return totp.VerifyTotp(otpCode.ToString(), out _);
        }
    }
}
