using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.User.Commands.CreateUser;

public class CreateUserCommand: IRequest<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IPasswordCryptographyService _passwordCryptographyService;

    public CreateUserCommandHandler(IApplicationDbContext applicationDbContext, IPasswordCryptographyService passwordCryptographyService)
    {
        _applicationDbContext = applicationDbContext;
        _passwordCryptographyService = passwordCryptographyService;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordCryptographyService.HashPassword(request.Password);

        var user = new ApplicationUser()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = $"{request.FirstName} {request.LastName}",
            Phone = request.Phone,
            Password = hashedPassword
        };
        _applicationDbContext.ApplicationUsers.Add(user);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}