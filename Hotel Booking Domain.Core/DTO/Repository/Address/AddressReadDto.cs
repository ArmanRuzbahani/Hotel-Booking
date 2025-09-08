using Hotel_Booking_Domain.Core.DTO.Repository.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.Repository.Address
{
	public class AddressReadDto
	{
		public int Id { get; set; }
		public string AddressName { get; set; }
		public int CustomerId { get; set; }
		public CustomerReadDto? customerReadDto { get; set; }
	}
}
