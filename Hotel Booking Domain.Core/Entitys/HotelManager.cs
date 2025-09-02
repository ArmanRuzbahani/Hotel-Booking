using System.ComponentModel.DataAnnotations;

namespace Entitys_Hotel.Models
{
	public class HotelManager : User
	{
		
		public List<Hotel> hotels {  get; set; }
	}
}
