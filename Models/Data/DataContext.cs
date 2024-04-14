using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Financeiro;
using HefestusApi.Models.Produtos;
using HefestusApi.Models.Vendas;
using Microsoft.EntityFrameworkCore;
using HefestusApi.Models.Administracao;

namespace HefestusApi.Models.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Person> Person { get; set; }
        public DbSet<PersonGroup> PersonGroup { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductFamily> ProductFamily { get; set; }
        public DbSet<ProductSubGroup> ProductSubGroup { get; set; }

        public DbSet<PaymentOptions> PaymentOptions { get; set; }
        public DbSet<PaymentCondition> PaymentCondition { get; set; }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }

        public DbSet<OrderInstallment> OrderInstallment { get; set; }

        public DbSet<Stock> Stock { get; set; }

        public DbSet<SystemLocation> SystemLocation { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemLocation>()
                .HasOne(s => s.Person)
                .WithOne(p => p.SystemLocation)
                .HasForeignKey<SystemLocation>(s => s.PersonId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Person)
                .WithOne(p => p.User)
                .HasForeignKey<User>(u => u.PersonId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.DefaultLocation)
                .WithMany(s => s.Users)
                .HasForeignKey(s => s.SystemLocationId);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Product)
                .WithMany(p => p.Stocks)
                .HasForeignKey(s => s.ProductId);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.SystemLocation)
                .WithMany(p => p.Stocks)
                .HasForeignKey(s => s.SystemLocationId);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.City)
                .WithMany(c => c.Persons)
                .HasForeignKey(p => p.CityId);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.SystemLocation)
                .WithOne(p => p.Person)
                .HasForeignKey<Person>(p => p.SystemLocationId);

            modelBuilder.Entity<Person>()
               .HasOne(p => p.User)
               .WithOne(c => c.Person)
               .HasForeignKey<Person>(p => p.UserId);

            modelBuilder.Entity<Person>()
               .HasIndex(p => p.Name)
               .HasDatabaseName("IX_Persons_Name")
               .IsUnique(false);


            modelBuilder.Entity<PersonGroup>()
               .HasIndex(p => p.Name)
               .HasDatabaseName("IX_PersonGroups_Name")
               .IsUnique(false);

            modelBuilder.Entity<City>()
                .HasIndex(c => c.Name)
                .HasDatabaseName("IX_Cities_Name")
                .IsUnique(false);

            modelBuilder.Entity<Product>()
                .HasIndex(c => c.Name)
                .HasDatabaseName("IX_Products_Name")
                .IsUnique(false);

            modelBuilder.Entity<ProductGroup>()
               .HasIndex(c => c.Name)
               .HasDatabaseName("IX_ProductGroups_Name")
               .IsUnique(false);

            modelBuilder.Entity<ProductFamily>()
               .HasIndex(c => c.Name)
               .HasDatabaseName("IX_ProductFamilies_Name")
               .IsUnique(false);

            modelBuilder.Entity<ProductSubGroup>()
               .HasIndex(c => c.Name)
               .HasDatabaseName("IX_ProductSubGroups_Name")
               .IsUnique(false);

            modelBuilder.Entity<PaymentCondition>()
              .HasIndex(c => c.Name)
              .HasDatabaseName("IX_PaymentConditions_Name")
              .IsUnique(false);

            modelBuilder.Entity<PaymentOptions>()
               .HasIndex(c => c.Name)
               .HasDatabaseName("IX_PaymentOptions_Name")
               .IsUnique(false);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Family)
                .WithMany()
                .HasForeignKey(p => p.FamilyId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Group)
                .WithMany()
                .HasForeignKey(p => p.GroupId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Subgroup)
                .WithMany()
                .HasForeignKey(p => p.SubgroupId);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.Id });

            modelBuilder.Entity<OrderInstallment>()
                .HasKey(op => new { op.OrderId, op.InstallmentNumber });

            modelBuilder.Entity<OrderInstallment>()
               .HasOne(op => op.Order)
               .WithMany(o => o.OrderInstallments)
               .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(c => c.OrderProducts)
                .HasForeignKey(op => op.ProductId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Responsible)
                .WithMany()
                .HasForeignKey(o => o.ResponsibleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
