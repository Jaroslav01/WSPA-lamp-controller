namespace CleanArchitecture.Application.Common.Interfaces;

public interface IPasswordCryptographyService
{
    public string HashPassword(string password);
    public bool VerifyHashedPassword(string hashedPassword, string providedPassword);
}