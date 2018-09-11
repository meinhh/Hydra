using System.Collections.Generic;
using System;

namespace Hydra.Models
{
	public class Order
	{
		public int ID { get; set; }

		public ICollection<ProductInStore> ProductsInStore { get; set; }

		public DateTime Date { get; set; }

        public User Buyer { get; set; }

        public PaymentType PaymentType { get; set; }
	}
}