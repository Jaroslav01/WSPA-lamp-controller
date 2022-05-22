using MQTTnet.Client;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IMqttService
{
    public Task<MqttClientConnectResult> ConnectAsync(CancellationToken cancellationToken);
    public Task<MqttClientPublishResult> PublishAsync(
        string payload, string topic, CancellationToken cancellationToken
    );
    public Task<MqttClientSubscribeResult> SubscribeAsync(string topic, CancellationToken cancellationToken);

}