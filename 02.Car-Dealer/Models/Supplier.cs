﻿namespace _02.Car_Dealer.Models
{
    using System.Collections.Generic;

    public class Supplier
    {
        public Supplier()
        {
            this.Parts = new HashSet<Part>();
        }

        public int SupplierId { get; set; }

        public string Name { get; set; }

        public bool IsImporter { get; set; }

        public virtual ICollection<Part> Parts { get; set; }
    }
}
