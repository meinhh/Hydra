using System.Collections.Generic;
using System;

namespace Hydra.Models
{
	public class Order
	{
		public int ID { get; set; }

		public ICollection<Product> Products { get; set; }

		public DateTime Date { get; set; }

		public Employee Seller { get; set; }

        public Customer Buyer { get; set; }

        public PaymentType PaymentType { get; set; }
	}
}