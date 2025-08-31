using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entitys_Hotel.Models
{
	public class HotelEnum
	{

		public enum RoomType
		{
			[Display(Name = "یک تخته")]
			Single=1,     

			[Display(Name = "دو تخته")]
			Double=2,  

			[Display(Name = "سه تخته")]
			Triple=3,  

			[Display(Name = "چهار تخته")]
			Quad    =4    
		}

		public enum IranCityForHotel
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
	}
}
