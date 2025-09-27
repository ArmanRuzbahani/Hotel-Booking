using System;

namespace Hotel_Booking_Domain.Core.Entitys.Service
{
	public class Captcha
	{
		public string Question { get; set; }
		public int Answer { get; set; }

		private static Random _random = new Random();

		
		public static Captcha GenerateMathCaptcha()
		{
			int a = _random.Next(1, 20);
			int b = _random.Next(1, 20);
			return new Captcha
			{
				Question = $"{a} + {b} = ؟",
				Answer = a + b
			};
		}
	}
}
