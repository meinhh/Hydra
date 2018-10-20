using Microsoft.EntityFrameworkCore;
using Hydra.Models;

namespace Hydra.Data
{
	public class HydraContext : DbContext
	{
		public HydraContext(DbContextOptions<HydraContext> options)
			: base(options)
		{
            Database.Migrate();
        }

		public DbSet<Product> Product { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Stock> Stock { get; set; }
    }
}