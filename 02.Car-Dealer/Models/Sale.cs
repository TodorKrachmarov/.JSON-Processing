using System.ComponentModel.DataAnnotations.Schema;

namespace _02.Car_Dealer.Models
{

    public class Sale
    {
        public int SaleId { get; set; }

        public int CarId { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public decimal DiscountPercentage { get; set; }
    }
}
