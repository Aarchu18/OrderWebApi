using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OrderManagementApi.Models
{
    public partial class OrderManagementDBContext : DbContext
    {
        public OrderManagementDBContext()
        {
        }

        public OrderManagementDBContext(DbContextOptions<OrderManagementDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoryMaster> CategoryMaster { get; set; }
        public virtual DbSet<ClientMaster> ClientMaster { get; set; }
        public virtual DbSet<ItemMaster> ItemMaster { get; set; }
        public virtual DbSet<OrderMaster> OrderMaster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=KELLGGNCPU0229\\SQLEXPRESS;Database=OrderManagementDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryMaster>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientMaster>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.ClientAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ItemMaster>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.Property(e => e.ItemId)
                    .HasColumnName("ItemID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ItemCategory)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderMaster>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.OrderDate).HasColumnType("date");
            });
        }
    }
}
