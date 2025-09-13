using Entitys_Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.Repository.HotelManager
{
	public class HotelManagerUpdateDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string? Email { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string PhoneNumber { get; set; }
		public string CardId { get; set; }
		public UserJob? Job { get; set; }
		public UserMarid? MaritalStatus { get; set; }
		public UserGender? Gender { get; set; }
		public UserEducation? Education { get; set; }
		public UserNationality? Nationality { get; set; }
		public IranCity? City { get; set; }
	}
}
