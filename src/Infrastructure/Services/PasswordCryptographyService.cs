using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Services;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
public class PasswordCryptographyService: IPasswordCryptographyService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 12);
    }
	
    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        bool isValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);

        /*
        if (isValid && BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, 12))
        {
            return PasswordVerificationResult.SuccessRehashNeeded;
        }
        */
        
        return isValid;
    }
}