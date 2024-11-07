namespace OrderManagement.Business.Services.Abstracts
{
    public interface IEncryptionService
    {
        string Encrypt(string input);
        string Decrypt(string encryptedInput);
    }
}
