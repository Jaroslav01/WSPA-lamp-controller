using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.User.Queries.Dto;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.User.Queries.GetUserById;

public class GetUserByIdQuery: IRequest<ApplicationUserDto>
{
    public Guid Id { get; set; }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApplicationUserDto>
{
    private IApplicationDbContext _applicationDbContext;

    public GetUserByIdQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public Task<ApplicationUserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = _applicationDbContext.ApplicationUsers.FirstOrDefault(user => user.Id == request.Id);
        if(user == null) throw new NotFoundException($"User by Id: {request.Id}, not found.");
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
