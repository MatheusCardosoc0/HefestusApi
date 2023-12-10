using HefestusApi.Models.Administracao;
using HefestusApi.Models.Produtos;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Utils
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Person)
                .WithOne(p => p.User)
                .HasForeignKey<User>(u => u.PersonId);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.City)
                .WithMany(c => c.Persons)
                .HasForeignKey(p => p.CityId);

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
        }
    }
}
