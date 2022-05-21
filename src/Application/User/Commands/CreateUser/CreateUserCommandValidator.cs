using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.User.Queries.GetUserById;
using FluentValidation;

namespace CleanArchitecture.Application.User.Commands.CreateUser;

public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
{
    private readonly IApplicationDbContext _context;

    private bool UniqueEmail(string email)
    {
        var user = _context.ApplicationUsers.FirstOrDefault(user => user.Email == email);
        return user == null;
    }
    private bool UniquePhone(string phone)
    {
        var user = _context.ApplicationUsers.FirstOrDefault(user => user.Phone == phone);
        return user == null;
    }
    public CreateUserCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        
        RuleFor(v => v.FirstName)
            .NotEmpty();
        RuleFor(v => v.LastName)
            .NotEmpty();
        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress()
            .Must(UniqueEmail);
        RuleFor(v => v.Password)
            .NotEmpty()
            .MinimumLength(8);
        RuleFor(v => v.Phone)
            .NotEmpty()
            .MinimumLength(10)
            .Must(UniquePhone);
    }
}
