using System.Linq;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.User.Queries.Dto;
using CleanArchitecture.Application.User.Queries.GetUserById;
using MediatR;

namespace CleanArchitecture.Application.User.Queries.GetCurrentUser;

public class GetCurrentUserQuery: IRequest<ApplicationUserDto>
{
    
}

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, ApplicationUserDto>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;

    public GetCurrentUserQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
    }

    public Task<ApplicationUserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = _applicationDbContext.ApplicationUsers.FirstOrDefault(user => user.Id == _currentUserService.UserIdGuid);
        if(user == null) throw new NotFoundException($"User by Id: {_currentUserService.UserId}, not found.");
        var userDto = new ApplicationUserDto()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Phone = user.Phone,
            FirstNmae = user.FirstName,
            LastNmae = user.LastName
        };
        return Task.FromResult(userDto);
    }
}