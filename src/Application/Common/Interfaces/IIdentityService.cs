using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IIdentityService
{
    public string GenerateJwtToken(ApplicationUser user);
}
