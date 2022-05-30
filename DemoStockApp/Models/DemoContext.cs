using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DemoStockApp.Models
{
    public partial class DemoContext : DbContext
    {
   

        public DemoContext(DbContextOptions<DemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<StocksOrders> StocksOrders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StocksOrders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("stocksOrders");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasColumnName("customerName")
                    .HasMaxLength(50);

                entity.Property(e => e.PurchaseDate)
                    .HasColumnName("purchaseDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.QuantityPurchased).HasColumnName("quantityPurchased");

                entity.Property(e => e.StockCurrentPrice).HasColumnName("stockCurrentPrice");

                entity.Property(e => e.StockName)
                    .IsRequired()
                    .HasColumnName("stockName")
                    .HasMaxLength(50);

                entity.Property(e => e.StockPurchasePrice).HasColumnName("stockPurchasePrice");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
