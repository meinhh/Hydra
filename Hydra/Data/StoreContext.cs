using Microsoft.EntityFrameworkCore;
using Hydra.Models;

namespace Hydra.Data
{
	public class StoreContext : DbContext
	{
		public StoreContext(DbContextOptions<StoreContext> options)
			: base(options)
		{
		}

		public DbSet<Product> Product { get; set; }
	}
}