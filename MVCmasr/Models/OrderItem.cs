using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCmasr.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Album")]
        public int AlbumId { get; set; }
        public Album Album { get; set; }


        [Range(0, 500)]
        [Required]
        public int Quantity { get; set; }

        [Range(0,10000)]
        public decimal Price { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
