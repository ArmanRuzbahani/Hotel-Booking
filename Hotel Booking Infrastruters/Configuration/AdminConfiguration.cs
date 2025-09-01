using Entitys_Hotel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Infrastruters.Configuration
{
	public class AdminConfiguration : IEntityTypeConfiguration<Admin>
	{
		public void Configure(EntityTypeBuilder<Admin> builder)
		{
			builder.HasData(new List<Admin>()
		{
			new Admin()
			{
				Id = 1,
				Name = "آرمان",
				LastName = "روزبهانی",
				Email = "arman.ruzb@gmail.com",
				DateOfBirth = new DateTime(2005, 7, 21),
				PhoneNumber = "+989123456789",
				CardId = "1234567890123456",
				UserCreateAt = new DateTime(2025, 8, 18),
				IsActive = true,
				Job = UserJob.Manager,
				MaritalStatus = UserMarid.Married,
				Gender = UserGender.Male,
				Education = UserEducation.Master,
				Nationality = UserNationality.Iranian,
				City = IranCity.Tehran,
				Role = UserRole.Admin
			}
		});
		}
	}
}
