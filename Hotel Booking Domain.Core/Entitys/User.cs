using System;
using System.ComponentModel.DataAnnotations;

namespace Entitys_Hotel.Models
{
	public class User
	{
		
		public int Id { get; set; }

		public string? Name { get; set; }

		public string? LastName { get; set; }

		public string? Email { get; set; }

		public DateTime DateOfBirth { get; set; }

		public int? Age => (int)((DateTime.Now - DateOfBirth).TotalDays / 365.25);
			
		public string PhoneNumber { get; set; }

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
