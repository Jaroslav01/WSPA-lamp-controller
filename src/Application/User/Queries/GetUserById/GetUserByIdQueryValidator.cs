using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.User.Queries.GetUserById;

public class GetUserByIdQueryValidator: AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
    }

}