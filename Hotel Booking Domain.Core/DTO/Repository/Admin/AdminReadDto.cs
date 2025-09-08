using Entitys_Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.Repository.Admin
{
	public class AdminReadDto
	{
		public int Id { get; set; }

		public string? Name { get; set; }

		public string? LastName { get; set; }

		public string? Email { get; set; }

		public bool IsEmailVerfied { get; set; } = false;

		public DateTime DateOfBirth { get; set; }

		public int? Age => (int)((DateTime.Now - DateOfBirth).TotalDays / 365.25);

		public string PhoneNumber { get; set; }

		public bool IsPhoneNumberVerfied { get; set; } = false;

		public string CardId { get; set; }

		public DateTime UserCreateAt { get; set; } = DateTime.Now;

		public bool IsActive { get; set; }

		public UserJob? Job { get; set; }

		public UserMarid? MaritalStatus { get; set; }

		public UserGender? Gender { get; set; }

		public UserEducation? Education { get; set; }

		public UserNationality? Nationality { get; set; }

		public IranCity? City { get; set; }

		public UserRole Role { get; set; }
	}
}
