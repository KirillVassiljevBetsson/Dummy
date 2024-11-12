using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Techsson.Gaming.Infrastructure.KafkaProducerLibrary;

namespace Publisher;

class Program
{
    static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false).Build();
        var kafkaConsumerConfiguration = config.GetSection(nameof(KafkaProducerConfiguration)).Get<KafkaProducerConfiguration>()!;

        HostApplicationBuilder hostBuilder = Host.CreateApplicationBuilder(args);
        hostBuilder.Services.AddLogging();
        hostBuilder.Services.SetupKafkaProducer(kafkaConsumerConfiguration);
        hostBuilder.Services.AddHostedService<KafkaHostedService>();

        var host = hostBuilder.Build();
        await host.RunAsync();
    }
}
