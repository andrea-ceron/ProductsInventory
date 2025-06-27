using AutoMapper;
using ProductsInventory.Business.helperModels;
using ProductsInventory.Repository.Models;
using ProductsInventory.Shared.DTO;
using PurchaseManager.Shared.DTO;
using System.Diagnostics.CodeAnalysis;


namespace CustomerManager.Business.Profiles;

/// <summary>
/// Marker per <see cref="AutoMapper"/>.
/// </summary>
public sealed class AssemblyMarker
{
	AssemblyMarker() { }
}

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class InputFileProfile : Profile
{
	public InputFileProfile()
	{
		CreateMap<CreateRawMaterialDto, RawMaterial>();
		CreateMap<RawMaterial, ReadAndUpdateRawMaterialDto>();
		CreateMap<ReadAndUpdateRawMaterialDto, RawMaterial>();

		CreateMap<CreateEndProductDto, EndProduct>();
		CreateMap<EndProduct, ReadEndProductDto>();
		CreateMap<UpdateEndProductDto, EndProduct>();
		CreateMap<EndProduct, EndProductDtoForKafka>();

		CreateMap<CreateRawMaterialForProductionDto, RawMaterialForProduction>();
		CreateMap<RawMaterialForProduction, ReadRawMaterialForProductionDto>();
		CreateMap<UpdateRawMaterialForProductionDto, RawMaterialForProduction>();
		CreateMap<CreateRawMaterialForProductionHelperDto, RawMaterialForProduction>();


		CreateMap<CreateShipmentDto, Shipment>();
		CreateMap<Shipment, ReadShipmentDto>();
		CreateMap<UpdateShipmentDto, Shipment>();

		CreateMap<CreateShippingItemsDto, ShipmentItems>()
			.ForMember(dest => dest.Id, opt => opt.Ignore());
		CreateMap<ShipmentItems, ReadShippingItemsDto>();
		CreateMap<UpdateShippingItemsDto, ShipmentItems>();

		CreateMap<CreateProductionProcessDto, ProductionProcess>();
		CreateMap<ProductionProcess, ReadProductionProcessDto>();

		CreateMap<ProductDtoForKafka, RawMaterial>();


	}
}