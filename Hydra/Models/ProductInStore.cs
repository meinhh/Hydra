namespace Hydra.Models
{
    public class ProductInStore
    {
        public int ID { get; set; }
        public Product Product { get; set; }
        public Store Store { get; set; }
        public int Quantity { get; set; }
    }
}
