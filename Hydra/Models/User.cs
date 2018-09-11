namespace Hydra.Models
{
	public class User
	{
		public int ID { get; set; }

		public string Name { get; set; }

		public string Address { get; set; }

		public string Phone { get; set; }

        public bool IsManager { get; set; }
    }
}