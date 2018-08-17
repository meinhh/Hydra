using System.Collections.Generic;

namespace Hydra.Models
{
	public class Store
	{
		public int ID { get; set; }

		public string Address { get; set; }

		public ICollection<Product> Products { get; set; }

		public ICollection<Employee> Employees { get; set; }

		public ICollection<Order> Orders { get; set; }
	}
}