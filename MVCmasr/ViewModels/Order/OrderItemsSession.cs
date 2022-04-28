using MVCmasr.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCmasr.ViewModels.Order
{
    public class OrderItemsSession
    {
        [NotMapped]
        private int _quantity;
        public int Id { get; set; }
        public string UserId { get; set; }
        public int AlbumId { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { 
            get { return _quantity; } 
            set {
                if(value < 0)
				{
                    _quantity = 0;
				}
				_quantity = value;
            } }
        public decimal Price { get; set; }

        [NotMapped]
        public decimal TotalPrice => Price * Quantity;

		[NotMapped]
		public Album Album { get; set; } = new Album();
	}
}
