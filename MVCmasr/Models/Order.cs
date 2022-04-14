using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCmasr.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string ShippingAddress { get; set; }
        public string Phone { get; set; }
        
        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

    }
}
