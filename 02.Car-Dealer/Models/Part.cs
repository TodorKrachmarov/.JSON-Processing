namespace _02.Car_Dealer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Part
    {
        public Part()
        {
            this.Cars = new HashSet<Car>();
        }

        public int PartId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }
    }
}
