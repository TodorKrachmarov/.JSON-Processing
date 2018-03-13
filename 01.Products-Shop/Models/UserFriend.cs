namespace _01.Products_Shop.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserFriend
    {
        [Key]
        [Column(Order = 1)]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Key]
        [Column(Order = 2)]
        public int FriendId { get; set; }

        [ForeignKey("FriendId")]
        public virtual User Friend { get; set; }
    }
}
