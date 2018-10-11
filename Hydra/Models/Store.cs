using System;
using System.Collections.Generic;

namespace Hydra.Models
{
	public class Store
	{
		public int ID { get; set; }

        public string Name { get; set; }

        public double Lontitude { get; set; }

        public double Latitude { get; set; }

        public DateTime OpeningHour { get; set; }

        public DateTime ClosingHour { get; set; }

        public ICollection<Stock> Stock { get; set; }

        public ICollection<Order> Orders { get; set; }
	}
}