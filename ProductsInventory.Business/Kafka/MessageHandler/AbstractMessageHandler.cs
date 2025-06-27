using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductsInventory.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Kafka.ExceptionManager;
using Utility.Kafka.MessageHandlers;

namespace ProductsInventory.Business.Kafka.MessageHandler;

public abstract class AbstractMessageHandler<TMessageDTO, TDomainDto>
	(ErrorManagerMiddleware errorManager, IMapper map) 
	: OperationMessageHandlerBase<TMessageDTO>(errorManager)
	where TMessageDTO : class
{
	protected override async Task InsertAsync(TMessageDTO messageDto, CancellationToken cancellationToken = default)
	{
		await InsertDto(map.Map<TDomainDto>(messageDto), cancellationToken);
	}

	protected override async Task UpdateAsync(TMessageDTO messageDto, CancellationToken cancellationToken = default)
	{
		await UpdateDto(map.Map<TDomainDto>(messageDto), cancellationToken);

	}
	protected override async  Task DeleteAsync(TMessageDTO messageDto, CancellationToken cancellationToken = default)
	{
		await DeleteDto(map.Map<TDomainDto>(messageDto), cancellationToken);
	}
	protected abstract Task InsertDto(TDomainDto? domainDto, CancellationToken ct = default);
	protected abstract Task UpdateDto(TDomainDto? messageDto, CancellationToken ct = default);
	protected abstract Task DeleteDto(TDomainDto? messageDto, CancellationToken ct = default);

}

