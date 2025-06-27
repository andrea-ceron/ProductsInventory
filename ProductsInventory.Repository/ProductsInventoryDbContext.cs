using Microsoft.EntityFrameworkCore;
using ProductsInventory.Repository.Model;
using ProductsInventory.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsInventory.Repository
{
    public class ProductsInventoryDbContext(DbContextOptions<ProductsInventoryDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<RawMaterial>()
				.HasKey(p => p.Id);
			modelBuilder.Entity<RawMaterial>()
				.HasMany(r => r.RawMaterialForProduction)
				.WithOne(r => r.RawMaterial)
				.HasForeignKey(r => r.RawMaterialId);

			modelBuilder.Entity<ProductionProcess>()
				.HasKey(p => p.Id);

			modelBuilder.Entity<EndProduct>()
				.HasKey(p => p.Id);
			modelBuilder.Entity<EndProduct>()
				.HasMany(p => p.ProductionProcess)
				.WithOne(e => e.EndProduct)
				.HasForeignKey(e => e.EndProductId);
			modelBuilder.Entity<EndProduct>()
				.HasMany(p => p.RawMaterialForProduction)
				.WithOne(e => e.EndProduct)
				.HasForeignKey(e => e.EndProductId);

			modelBuilder.Entity<Shipment>()
				.HasKey(p => p.Id);
			modelBuilder.Entity<Shipment>()
				.HasMany(p => p.Items)
				.WithOne(e => e.Shipment)
				.HasForeignKey(e => e.ShipmentId);	

			modelBuilder.Entity<RawMaterialForProduction>()
				.HasKey(p => p.Id);
			
			modelBuilder.Entity<ShipmentItems>()
				.HasKey(p => p.Id);

			modelBuilder.Entity<TransactionalOutbox>()
				.HasKey(p => p.Id);

		}
		public DbSet<RawMaterial> RawMaterials { get; set; }
		public DbSet<ProductionProcess> ProductionProcesses { get; set; }
		public DbSet<EndProduct> EndProducts { get; set; }
		public DbSet<Shipment> Shipments { get; set; }
		public DbSet<RawMaterialForProduction> RawMaterialForProductions { get; set; }
		public DbSet<ShipmentItems> ShippingItems { get; set; }
		public DbSet<TransactionalOutbox> TransactionalOutboxes { get; set; }

	}
}
