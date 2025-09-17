using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.Repository.Address
{
	public class AddressUpdateDto
	{
		public int Id { get; set; }
		[Required]
		[Display(Name ="آدرس")]
		[Range(10, 200, ErrorMessage = "بین 100 تا 200")]
		public string AddressName { get; set; }
		public int CustomerId { get; set; }
	}
}
