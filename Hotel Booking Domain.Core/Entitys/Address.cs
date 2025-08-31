using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entitys_Hotel.Models
{
	public class Address
	{
		[Key]  
		public int Id { get; set; }

		public string AdressName { get; set; }

		[ForeignKey("User")]  
		public int UserId { get; set; }

		public User User { get; set; }
	}

}
