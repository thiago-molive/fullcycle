using Confluent.Kafka;
using EDA_FC3.Application.Contracts;
using EDA_FC3.Application.Queries;
using MassTransit;
using static EDA_FC3.Infra.Integration.KafkaConsume;

namespace EDA_FC3.Initializers;

public static class AddMassTransitConfiguration
{
    public static void AddMassTransitConfig(this IServiceCollection services, IConfiguration config)
    {
        services.AddMassTransit(x =>
        {
            AddAccountsCommandInitializer(x);
            AddKafkaConsume(x, config);
        });
    }

    private static void AddAccountsCommandInitializer(IBusRegistrationConfigurator x)
    {
        // configure the consumer on a specific endpoint address
        x.AddConsumer<GetBalanceQueryHandler>();

        x.AddConsumer<GetBalanceByIdQueryHandler>();

        x.AddMediator(cfg =>
        {
            cfg.AddConsumer<GetBalanceQueryHandler>();
            cfg.AddConsumer<GetBalanceByIdQueryHandler>();
        });

        // Sends the request to the specified address, instead of publishing it
        x.AddRequestClient<GetBalancesQuery>();
        x.AddRequestClient<GetBalanceByIdQuery>();


        x.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });
    }

    public static void AddKafkaConsume(IBusRegistrationConfigurator x, IConfiguration config)
    {
        string KAFKA_HOST = config.GetValue<string>("kafka:host") ?? "kafka:29092";
        string GROUP = config.GetValue<string>("kafka:group") ?? "balances";
        const string TOPIC = "balances";

        x.AddRider(rider =>
        {
            rider.AddConsumer<KafkaMessageConsumer>(configure =>
            {
                configure.UseMessageRetry(retryConfig =>
                {
                    retryConfig.Interval(5, TimeSpan.FromSeconds(10)); // 5 tentativas com intervalo de 10 segundos
                });
            });

            rider.UsingKafka((context, k) =>
            {
                k.Host(KAFKA_HOST);

                k.TopicEndpoint<KafkaMessage>(TOPIC, GROUP, e =>
                {
                    e.ConfigureConsumer<KafkaMessageConsumer>(context);

                    // Configurando reconexão e re-tentativa no endpoint
                    e.UseMessageRetry(retryConfig =>
                    {
                        retryConfig.Interval(5, TimeSpan.FromSeconds(10)); // 5 tentativas com intervalo de 10 segundos
                    });

                    e.AutoOffsetReset = AutoOffsetReset.Earliest;
                });
            });
        });
    }
}