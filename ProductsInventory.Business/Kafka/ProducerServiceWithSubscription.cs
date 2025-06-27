using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProductsInventory.Business.Abstractions;
using ProductsInventory.Repository.Abstractions;
using ProductsInventory.Repository.Model;
using ProductsInventory.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Kafka.Abstraction.Clients;
using Utility.Kafka.ExceptionManager;
using Utility.Kafka.Services;

namespace ProductsInventory.Business.Kafka;

public class ProducerServiceWithSubscription(
	IServiceProvider serviceProvider,
	ErrorManagerMiddleware errormanager,
	IOptions<KafkaTopicsOutput> optionTopics
	, IServiceScopeFactory serviceScopeFactory
	, IProducerClient<string, string> producerClient
	, IEndProductObservable observable

	)
	: Utility.Kafka.Services.ProducerServiceWithSubscription(serviceProvider, errormanager)
{
	protected override IEnumerable<string> GetTopics()
	{
		return optionTopics.Value.GetTopics();
	}

	protected async  override Task OperationsAsync(CancellationToken cancellationToken)
	{
		using IServiceScope scope = serviceScopeFactory.CreateScope();
		IRepository repository = scope.ServiceProvider.GetRequiredService<IRepository>();
		IEnumerable<TransactionalOutbox> transactionalOutboxes = (await repository.GetAllTransactionalOutbox(cancellationToken)).OrderBy(x => x.Id);
		if (!transactionalOutboxes.Any())
		{
			//logger.LogInformation($"Non ci sono TransactionalOutbox da elaborare");
			return;
		}
		foreach (TransactionalOutbox elem in transactionalOutboxes)
		{
			string topic = elem.Table switch
			{
				nameof(EndProduct) => optionTopics.Value.EndProduct,
				_ => throw new ArgumentOutOfRangeException($"La tabella {elem.Table} non è prevista come topic nel Producer")
			};
			try
			{
				await producerClient.ProduceAsync(topic, elem.Id.ToString(), elem.Message, null, cancellationToken);
				await repository.DeleteTransactionalOutboxAsync(elem.Id, cancellationToken);
				await repository.SaveChangesAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				//logger.LogError(ex, "Errore durante la produzione del messaggio per il topic {topic} con id {id}", topic, elem.Id);
				continue;
			}

			//logger.LogInformation("Eliminazione {groupMsg}...", groupMsg);

			//logger.LogInformation("Record eliminato");
		}
	}

	protected override IDisposable Subscribe(TaskCompletionSource<bool> tcs)
	{
		return observable.AddEndProduct.Subscribe((change) => tcs.TrySetResult(true));
	}
}
