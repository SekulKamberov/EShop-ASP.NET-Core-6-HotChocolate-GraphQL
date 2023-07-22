namespace EShop.Models
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public string CartId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDelivered { get; set; }
      

        public Cart Cart { get; set; } 
        public User User { get; set; }
    }
}
