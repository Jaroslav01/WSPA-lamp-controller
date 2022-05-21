using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.User.Queries.GetJwtToken;

public class GetJwtTokenQuery: IRequest<GetJwtTokenQueryDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class GetJwtTokenQueryHandler : IRequestHandler<GetJwtTokenQuery, GetJwtTokenQueryDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly IPasswordCryptographyService _passwordCryptographyService;

    public GetJwtTokenQueryHandler(IApplicationDbContext context, IIdentityService identityService, IPasswordCryptographyService passwordCryptographyService)
    {
        _context = context;
        _identityService = identityService;
        _passwordCryptographyService = passwordCryptographyService;
    }

    public Task<GetJwtTokenQueryDto> Handle(GetJwtTokenQuery request, CancellationToken cancellationToken)
    {
        var user = _context.ApplicationUsers.FirstOrDefault(
            user => user.Email == request.Email
        );
        bool passwordIsValid = _passwordCryptographyService.VerifyHashedPassword(user.Password, request.Password);
        if(user == null || !passwordIsValid) throw new NotImplementedException($"User by Email: {request.Email} and Password: {request.Password}, not found.");
        var jwtToken = _identityService.GenerateJwtToken(user);
        var response = new GetJwtTokenQueryDto()
        {
            Id = user.Id,
            FirstNmae = user.FirstName,
            LastNmae = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            Token = jwtToken,
            UserName = user.UserName
        };
        return Task.FromResult(response);
    }
}