using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Nemco
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Expens> Expenses { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Profit> Profits { get; set; }
        public virtual DbSet<Return> Returns { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<BillItem> BillItems { get; set; }
        public virtual DbSet<ReturnItem> ReturnItems { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>()
                .Property(e => e.DateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Bill>()
                .HasOptional(e => e.BillItem)
                .WithRequired(e => e.Bill);

            modelBuilder.Entity<Bill>()
                .HasOptional(e => e.Discount)
                .WithRequired(e => e.Bill);

            modelBuilder.Entity<Bill>()
                .HasOptional(e => e.Profit1)
                .WithRequired(e => e.Bill);

            modelBuilder.Entity<Bill>()
                .HasOptional(e => e.Sale)
                .WithRequired(e => e.Bill);

            modelBuilder.Entity<Expens>()
                .Property(e => e.DateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .HasOptional(e => e.Warehouse)
                .WithRequired(e => e.Item);

            modelBuilder.Entity<Profit>()
                .Property(e => e.DateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Return>()
                .Property(e => e.DateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Return>()
                .HasOptional(e => e.ReturnItem)
                .WithRequired(e => e.Return);

            modelBuilder.Entity<Sale>()
                .Property(e => e.DateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .HasOptional(e => e.Transaction)
                .WithRequired(e => e.Supplier);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.DateTime)
                .IsUnicode(false);
        }
    }
}
