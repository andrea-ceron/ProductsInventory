namespace ProductsInventory.Shared.DTO;

public class CreateRawMaterialDto
{
	public int InStorage { get; set; }
	public int SupplierId { get; set; }
	required public string ProductName { get; set; }
}


public class ReadAndUpdateRawMaterialDto
{
	public int Id { get; set; }
	public int InStorage { get; set; }
	public int SupplierId { get; set; }
	required public string ProductName { get; set; }
}