namespace DriveSafe.Domain.Security.Services;

public interface IEncryptService
{
    string Encrypt(string password);
    bool VerifyPassword(string password, string passwordhash);
}