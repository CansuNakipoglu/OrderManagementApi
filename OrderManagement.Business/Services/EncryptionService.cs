using Microsoft.AspNetCore.DataProtection;
using OrderManagement.Business.Services.Abstracts;

namespace OrderManagement.Business.Services
{
    public class EncryptionService: IEncryptionService
    {
        private readonly IDataProtector _protector;

        public EncryptionService(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("CansuProtectorSifre");
        }

        public string Encrypt(string input)
        {
            return _protector.Protect(input);
        }

        public string Decrypt(string encryptedInput)
        {
            try
            {
                return _protector.Unprotect(encryptedInput);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
