using Hotel_Booking_Domain.Core.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Infrastruters.Configuration
{
	public class FacilityConfiguration : IEntityTypeConfiguration<Facility>
	{
		public void Configure(EntityTypeBuilder<Facility> builder)
		{
			builder.HasData(new List<Facility>()
		{
			new Facility() { Id = 1, Name = "پارکینگ" },
			new Facility() { Id = 2, Name = "استخر" },
			new Facility() { Id = 3, Name = "اینترنت رایگان" },
			new Facility() { Id = 4, Name = "رستوران" },
			new Facility() { Id = 5, Name = "خشکشویی" },
			new Facility() { Id = 6, Name = "سالن ورزشی" },
			new Facility() { Id = 7, Name = "اتاق جلسات" },
			new Facility() { Id = 8, Name = "اسپا و ماساژ" },
			new Facility() { Id = 9, Name = "خدمات اتاق" },
			new Facility() { Id = 10, Name = "تلویزیون کابلی" },
			new Facility() { Id = 11, Name = "مینی‌بار" },
			new Facility() { Id = 12, Name = "سرویس فرودگاهی" },
			new Facility() { Id = 13, Name = "تسهیلات دسترسی برای افراد معلول" },
			new Facility() { Id = 14, Name = "خدمات 24 ساعته" },
			new Facility() { Id = 15, Name = "بار و کافی‌شاپ" },
			new Facility() { Id = 16, Name = "اتاق‌های ضد سیگار" },
			new Facility() { Id = 17, Name = "خدمات تور و گردشگری" },
			new Facility() { Id = 18, Name = "پارک بازی کودکان" },
			new Facility() { Id = 19, Name = "آسانسور" },
			new Facility() { Id = 20, Name = "کتابخانه" }
		});
		}
	}
}
