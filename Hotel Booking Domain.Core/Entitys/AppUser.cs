using Entitys_Hotel.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Entitys
{
	public class AppUser : IdentityUser<int>
	{
		public Customer Customer { get; set; }

		



		public Admin Admin { get; set; }

		



		public HotelManager HotelManager { get; set; }

		
	}
}
