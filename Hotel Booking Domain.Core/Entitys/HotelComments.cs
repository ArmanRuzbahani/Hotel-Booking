using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entitys_Hotel.Models
{
	public class HotelComments
	{
		[Key]
		public int Id { get; set; }

		
		public string Content { get; set; }

		
		public DateTime CreatedDateAt { get; set; }

		
		public int Rating { get; set; }

		public bool IsCheckingHotelManager { get; set; } = false;

		
		[ForeignKey("Hotel")] 
		public int HotelId { get; set; }

		public Hotel Hotel { get; set; }

		
		[ForeignKey("Customer")]
		public int CustomerId { get; set; }

		public Customer Customer { get; set; }
	}

}
