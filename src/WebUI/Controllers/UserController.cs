
using CleanArchitecture.Application.User.Commands.CreateUser;
using CleanArchitecture.Application.User.Queries.Dto;
using CleanArchitecture.Application.User.Queries.GetCurrentUser;
using CleanArchitecture.Application.User.Queries.GetJwtToken;
using CleanArchitecture.Application.User.Queries.GetUserById;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    [HttpGet]
    public ActionResult Test()
    {
        return Ok("TestOk");
    }
    
    [Authorize]
    [HttpPost("GetUserById")]
    public async Task<ApplicationUserDto> GetUserById(GetUserByIdQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [Authorize]
    [HttpGet("GetCurrentUser")]
    public async Task<ApplicationUserDto> GetCurrentUser()
    {
        return await Mediator.Send(new GetCurrentUserQuery());
    }
    
    [HttpPost("CreateUser")]
    public async Task<Guid> CreateUser(CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPost("GetJwtToken")]
    public async Task<GetJwtTokenQueryDto> GetJwtToken(GetJwtTokenQuery query)
    {
        return await Mediator.Send(query);
    }
}