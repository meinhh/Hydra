using System;

namespace Hydra.Models
{
	public class User
	{
		public int ID { get; set; }

		public string Name { get; set; }

        public Gender Gender { get; set; }

        public string email { get; set; }

        public bool IsManager { get; set; }

        public DateTime BirthDate { get; set; }
    }
}