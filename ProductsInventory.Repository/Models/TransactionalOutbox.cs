
using System.ComponentModel.DataAnnotations.Schema;
namespace ProductsInventory.Repository.Model;


public class TransactionalOutbox
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public long Id { get; set; }
	public string Table { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
}
