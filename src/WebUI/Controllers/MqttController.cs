using CleanArchitecture.Application.Mqtts.Command.Publish;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MQTTnet.Client;

namespace CleanArchitecture.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MqttController: ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    [HttpPost("Publish")]
    public async Task<MqttClientPublishResult> Publish(PublishCommand command)
    {
        return await Mediator.Send(command);
    }
}