using System.ComponentModel.DataAnnotations;

namespace Entitys_Hotel.Models
{
	public class HotelManager : User
	{
		[Required]
		public List<Hotel> hotels {  get; set; }
	}
}
