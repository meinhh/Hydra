using System.Collections.Generic;

namespace Hydra.Models
{
	public class Store
	{
		public int ID { get; set; }

		public string Address { get; set; }

        //public ICollection<Product> ProductsInStore { get; set; }

        public ICollection<Stock> Stock { get; set; }

        public ICollection<Order> Orders { get; set; }
	}
}