using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductsInventory.Repository.Abstractions;
using ProductsInventory.Repository.Models;
using PurchaseManager.Shared.DTO;
using Utility.Kafka.ExceptionManager;

namespace ProductsInventory.Business.Kafka.MessageHandler;

public class RawMaterialsKafkaMessageHandler
	(ILogger<RawMaterialsKafkaMessageHandler> logger,
	IRepository repository,
	IMapper map,
	ErrorManagerMiddleware errorManager) 
	: AbstractMessageHandler<ProductDtoForKafka, RawMaterial>(errorManager, map)
{
	protected override async Task DeleteDto(RawMaterial? message, CancellationToken ct = default)
	{
		await repository.DeleteRawMaterial(message.Id, ct);
		await repository.SaveChangesAsync(ct);
	}

	protected override async  Task InsertDto(RawMaterial? message, CancellationToken ct = default)
	{
		await repository.InsertRawMaterialAsync(message, ct);
		await repository.SaveChangesAsync(ct);
	}

	protected async override Task UpdateDto(RawMaterial? message, CancellationToken ct = default)
	{
		await repository.UpdateRawMaterialAsync(message, ct);
		await repository.SaveChangesAsync(ct);
	}
}
