using System.ComponentModel.DataAnnotations;

namespace Entitys_Hotel.Models
{
	public enum UserJob
	{
		[Display(Name = "دانشجو")]
		Student = 1,

		[Display(Name = "کارمند")]
		Employee = 2,

		[Display(Name = "مدیر")]
		Manager = 3,

		[Display(Name = "پزشک")]
		Doctor = 4,

		[Display(Name = "پرستار")]
		Nurse = 5,

		[Display(Name = "مهندس")]
		Engineer = 6,

		[Display(Name = "معلم")]
		Teacher = 7,

		[Display(Name = "استاد دانشگاه")]
		Professor = 8,

		[Display(Name = "وکيل")]
		Lawyer = 9,

		[Display(Name = "قاضی")]
		Judge = 10,

		[Display(Name = "پلیس")]
		PoliceOfficer = 11,

		[Display(Name = "سرباز")]
		Soldier = 12,

		[Display(Name = "راننده")]
		Driver = 13,

		[Display(Name = "کشاورز")]
		Farmer = 14,

		[Display(Name = "کارگر")]
		Worker = 15,

		[Display(Name = "آشپز")]
		Chef = 16,

		[Display(Name = "فروشنده")]
		Salesperson = 17,

		[Display(Name = "کارآفرین")]
		Entrepreneur = 18,

		[Display(Name = "برنامه‌نویس")]
		Programmer = 19,

		[Display(Name = "طراح گرافیک")]
		GraphicDesigner = 20,

		[Display(Name = "هنرمند")]
		Artist = 21,

		[Display(Name = "نویسنده")]
		Writer = 22,

		[Display(Name = "خبرنگار")]
		Journalist = 23,

		[Display(Name = "بازیگر")]
		Actor = 24,

		[Display(Name = "خواننده")]
		Singer = 25,

		[Display(Name = "ورزشکار")]
		Athlete = 26,

		[Display(Name = "مربی")]
		Coach = 27,

		[Display(Name = "خلبان")]
		Pilot = 28,

		[Display(Name = "مهماندار هواپیما")]
		FlightAttendant = 29,

		[Display(Name = "کاپیتان کشتی")]
		ShipCaptain = 30,

		[Display(Name = "مکانیک")]
		Mechanic = 31,

		[Display(Name = "الکتریک‌کار")]
		Electrician = 32,

		[Display(Name = "معمار")]
		Architect = 33,

		[Display(Name = "محقق")]
		Researcher = 34,

		[Display(Name = "کتابدار")]
		Librarian = 35,

		[Display(Name = "دانشمند")]
		Scientist = 36,

		[Display(Name = "پژوهشگر")]
		Scholar = 37,

		[Display(Name = "بازنشسته")]
		Retired = 38,

		[Display(Name = "بیکار")]
		Unemployed = 39,

		[Display(Name = "خانه‌دار")]
		Homemaker = 40,

		[Display(Name = "آزاد")]
		Freelancer = 41
	}
	public enum UserMarid
	{
		[Display(Name = "مجرد")]
		Single = 1,

		[Display(Name = "متاهل")]
		Married = 2,
	}
	public enum UserGender
	{
		[Display(Name = "مرد")]
		Male = 1,

		[Display(Name = "زن")]
		Female = 2,

		[Display(Name = "دیگر")]
		Other = 3
	}
	public enum UserEducation
	{
		[Display(Name = "زیر دیپلم")]
		BelowDiploma = 1,

		[Display(Name = "دیپلم")]
		Diploma = 2,

		[Display(Name = "کاردانی")]
		Associate = 3,

		[Display(Name = "کارشناسی")]
		Bachelor = 4,

		[Display(Name = "کارشناسی ارشد")]
		Master = 5,

		[Display(Name = "دکتری")]
		PhD = 6
	}
	public enum UserNationality
	{
		[Display(Name = "ایرانی")]
		Iranian = 1,

		[Display(Name ="اتباع")]
		Other = 2
	}
	public enum IranCity
	{
		[Display(Name = "تهران")]
		Tehran = 1,

		[Display(Name = "مشهد")]
		Mashhad = 2,

		[Display(Name = "اصفهان")]
		Isfahan = 3,

		[Display(Name = "شیراز")]
		Shiraz = 4,

		[Display(Name = "تبریز")]
		Tabriz = 5,

		[Display(Name = "کرج")]
		Karaj = 6,

		[Display(Name = "قم")]
		Qom = 7,

		[Display(Name = "اهواز")]
		Ahvaz = 8,

		[Display(Name = "رشت")]
		Rasht = 9,

		[Display(Name = "کرمان")]
		Kerman = 10,

		[Display(Name = "ارومیه")]
		Orumiyeh = 11,

		[Display(Name = "زاهدان")]
		Zahedan = 12,

		[Display(Name = "همدان")]
		Hamadan = 13,

		[Display(Name = "یزد")]
		Yazd = 14,

		[Display(Name = "اراک")]
		Arak = 15,

		[Display(Name = "بوشهر")]
		Bushehr = 16,

		[Display(Name = "سنندج")]
		Sanandaj = 17,

		[Display(Name = "بیرجند")]
		Birjand = 18,

		[Display(Name = "خرم‌آباد")]
		Khorramabad = 19,

		[Display(Name = "ساری")]
		Sari = 20,

		[Display(Name = "بندرعباس")]
		BandarAbbas = 21,

		[Display(Name = "قزوین")]
		Qazvin = 22,

		[Display(Name = "ایلام")]
		Ilam = 23,

		[Display(Name = "گرگان")]
		Gorgan = 24,

		[Display(Name = "شهرکرد")]
		ShahrKord = 25
	}
	public enum UserRole
	{
		[Display(Name = "مدیر سیستم")]
		Admin = 0,

		[Display(Name = "مشتری")]
		Customer = 1,

		[Display(Name = "مدیر هتل")]
		HotelManager = 2
	}
}
