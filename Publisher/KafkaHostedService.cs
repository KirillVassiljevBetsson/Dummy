using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Techsson.Gaming.Infrastructure.KafkaProducerLibrary;

namespace Publisher;

public class KafkaHostedService : IHostedService
{
    private readonly IKafkaProducer _kafkaProducer;
    private readonly ILogger _logger;

    public KafkaHostedService(IKafkaProducer kafkaProducer, ILogger<KafkaHostedService> logger)
    {
        _kafkaProducer = kafkaProducer;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var example = new NotificationExample { BrandId = "AlohaShark" };
        await _kafkaProducer.ProduceAsync(new EventEnvelope<NotificationExample>(example, new EventEnvelopeHeaders("NotificationExample", "Gpt-group", 1, true)), "gaming-gpt-events", cancellationToken);
        _logger.LogInformation($"Published message to kafka");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public class NotificationExample
{
    public string BrandId { get; set; }
}
