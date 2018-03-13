namespace _02.Car_Dealer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Car
    {
        public Car()
        {
            this.Parts = new HashSet<Part>();
            this.Sales = new HashSet<Sale>();
        }

        public int CarId { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public virtual ICollection<Part> Parts { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
