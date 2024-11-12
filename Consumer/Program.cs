using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Techsson.Gaming.Infrastructure.KafkaConsumerLibrary;

namespace Consumer;

class Program
{
    static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false).Build();
        var kafkaConsumerConfiguration = config.GetSection(nameof(KafkaConsumerConfiguration)).Get<KafkaConsumerConfiguration>()!;

        HostApplicationBuilder hostBuilder = Host.CreateApplicationBuilder(args);
        hostBuilder.Services.AddLogging();
        hostBuilder.Services.SetupKafkaConsumer(kafkaConsumerConfiguration, setupBuilder =>
        {
            setupBuilder.AddTypeMap<NotificationExample>("NotificationExample");
            setupBuilder.RegisterHandlersAssembly(typeof(Program));
        });

        var host = hostBuilder.Build();
        await host.RunAsync();
    }
}
