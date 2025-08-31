using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entitys_Hotel.Models
{
	public class HotelAddress
	{
		[Key]
		public int Id { get; set; }
        
		public string AddressName { get; set; }

		public string Address { get; set; }

		
		[ForeignKey("Hotel")] 
		public int HotelId { get; set; }

		public Hotel Hotel { get; set; }
	}

}
