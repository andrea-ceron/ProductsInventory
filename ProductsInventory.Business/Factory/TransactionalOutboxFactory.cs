using ProductsInventory.Repository.Model;
using ProductsInventory.Shared.DTO;

using System.Text.Json;
using Utility.Kafka.MessageHandlers;

namespace ProductsInventory.Business.Factory
{
    public static class TransactionalOutboxFactory
    {
		public static TransactionalOutbox CreateInsert(EndProductDtoForKafka dto) => Create(dto, Operations.Insert);
		public static TransactionalOutbox CreateUpdate(EndProductDtoForKafka dto) => Create(dto, Operations.Update);
		public static TransactionalOutbox CreateDelete(EndProductDtoForKafka dto) => Create(dto, Operations.Delete);

		private static TransactionalOutbox Create(EndProductDtoForKafka dto, string operation)
		{
			return Create(nameof(EndProductDtoForKafka), dto, operation);
		}

		private static TransactionalOutbox Create<TDTO>(string table, TDTO dto, string operation) where TDTO : class, new()
		{

			OperationMessage<TDTO> opMsg = new()
			{
				Dto = dto,
				Operation = operation
			};

			return new TransactionalOutbox()
			{
				Table = table,
				Message = JsonSerializer.Serialize(opMsg)
			};
		}
	}
}
