﻿namespace _01.Products_Shop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.ProductsSold = new HashSet<Product>();
            this.ProductsBought = new HashSet<Product>();
            this.Friends = new HashSet<UserFriend>();
        }

        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public virtual ICollection<Product> ProductsSold { get; set; }

        public virtual ICollection<Product> ProductsBought { get; set; }

        public virtual ICollection<UserFriend> Friends { get; set; }
    }
}
