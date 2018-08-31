using Microsoft.EntityFrameworkCore;
using Hydra.Models;

namespace Hydra.Data
{
	public class HydraContext : DbContext
	{
		public HydraContext(DbContextOptions<HydraContext> options)
			: base(options)
		{

		}

		public DbSet<Product> Product { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Order> Order { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(x => x.ID);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(x => x.ID);

                entity.HasOne<Store>(x => x.Store)
                    .WithMany(x => x.Employees)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("EMPLOYEE_STORE_FK");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.ID);
                entity.Property(x => x.ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(x => x.ID);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(x => x.ID);

                entity.HasMany<Product>(x => x.Products);
            });

        }
    }
}