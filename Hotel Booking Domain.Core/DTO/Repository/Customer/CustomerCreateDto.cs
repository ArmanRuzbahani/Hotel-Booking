using Entitys_Hotel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.Repository.Customer
{
	public class CustomerCreateDto
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string LastName { get; set; }

		[EmailAddress(ErrorMessage = "فرمت ایمیل نامعتبر است")]
		public string? Email { get; set; }
		public DateTime DateOfBirth { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		[Required]
		public string CardId { get; set; }
		public UserJob? Job { get; set; }
		public UserMarid? MaritalStatus { get; set; }
		public UserGender? Gender { get; set; }
		public UserEducation? Education { get; set; }
		public UserNationality? Nationality { get; set; }
		public IranCity city { get; set; }
		public UserRole Role { get; set; } = UserRole.Customer;

	}
}
