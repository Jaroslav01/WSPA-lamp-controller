using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace CleanArchitecture.Infrastructure.Services;

public class MqttService : IMqttService
{
    private static MqttFactory Factory { get; set; } = new MqttFactory();
    private static MqttClient MqttClient { get; set; } = Factory.CreateMqttClient();
    private readonly IConfiguration _configuration;
    private readonly ILogger<MqttService> _logger;

    public MqttService(IConfiguration configuration, ILogger<MqttService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<MqttClientConnectResult> ConnectAsync(CancellationToken cancellationToken)
    {
        _logger.Log(LogLevel.Information,"Mqtt connecting...");
        // Create TCP based options using the builder.
        var options = new MqttClientOptionsBuilder()
            .WithClientId(_configuration["MqttClient:ClientId"])
            .WithTcpServer(_configuration["MqttClient:TcpServer"], Int32.Parse(_configuration["MqttClient:Port"]))
            .WithCredentials(_configuration["MqttClient:Username"], _configuration["MqttClient:Password"])
            .WithCleanSession()
            .Build();
        
        var result = await MqttClient.ConnectAsync(options, cancellationToken);
        _logger.Log(LogLevel.Debug, result.ResultCode.ToString());
        await this.PublishAsync(".Net Core", ".Net", cancellationToken);
        return result;
    }

    public async Task<MqttClientPublishResult> PublishAsync(
        string payload, string topic, CancellationToken cancellationToken
    )
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.ExactlyOnce)
            .WithRetainFlag()
            .Build();
        
        return await MqttClient.PublishAsync(message, cancellationToken);
    }

    public async Task<MqttClientSubscribeResult> SubscribeAsync(string topic, CancellationToken cancellationToken)
    {
        // Subscribe to a topic
        return await MqttClient.SubscribeAsync(
            new MqttTopicFilterBuilder().WithTopic(topic).Build(), cancellationToken);
    }
}