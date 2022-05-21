using CleanArchitecture.Application.User.Queries.GetUserById;
using FluentValidation;

namespace CleanArchitecture.Application.User.Queries.GetJwtToken;

public class GetJwtTokenValidator: AbstractValidator<GetJwtTokenQuery>
{
    public GetJwtTokenValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(v => v.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}