using EDA_FC3.Domain;
using EDA_FC3.Infra.Persistence.Balances;
using MassTransit;

namespace EDA_FC3.Infra.Integration;

public static class KafkaConsume
{
    public class KafkaMessageConsumer : IConsumer<KafkaMessage>
    {
        private readonly IBalancesRepository _balancesRepository;

        public KafkaMessageConsumer(IBalancesRepository balancesRepository)
        {
            _balancesRepository = balancesRepository;
        }

        public async Task Consume(ConsumeContext<KafkaMessage> context)
        {
            var entity = Balances.Create(context.Message.Payload.Account_id_from
                , context.Message.Payload.Account_id_to
                , context.Message.Payload.Balance_account_id_from
                , context.Message.Payload.Balance_account_id_to);

            await _balancesRepository.Save(entity);
        }
    }

    public record KafkaMessage
    {
        public string Name { get; init; }
        public Payload Payload { get; set; }
    }

    public record Payload
    {
        public string Account_id_from { get; set; }
        public string Account_id_to { get; set; }
        public decimal Balance_account_id_from { get; set; }
        public decimal Balance_account_id_to { get; set; }
    }
}
