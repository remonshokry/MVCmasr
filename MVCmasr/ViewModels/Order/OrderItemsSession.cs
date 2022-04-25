using MVCmasr.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCmasr.ViewModels.Order
{
    public class OrderItemsSession
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int AlbumId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [NotMapped]
		public Album Album { get; set; } = new Album();
	}
}
