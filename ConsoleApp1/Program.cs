using System;
using System.Threading.Tasks;
using Hotel_Booking_Domain.Core.Entitys.Service;
using Hotel_Booking_Application_Service.Services;

class Program
{
	static async Task Main(string[] args)
	{
		Console.WriteLine("🚀 Captcha Service Test Start...\n");

		var captchaService = new CaptchaService();

		// 1️⃣ Generate new captcha
		var captcha = await captchaService.GenerateCaptchaAsync();
		Console.WriteLine($"Generated Captcha: Token={captcha.Token}, Code={captcha.Code}, IsUsed={captcha.IsUsed}, Attempts={captcha.Attempts}");

		// 2️⃣ Validate with wrong code
		bool wrongResult = captcha.Validate("wrong");
		Console.WriteLine($"Validate Wrong Code: {wrongResult}, Attempts={captcha.Attempts}");

		// 3️⃣ Validate with correct code
		bool correctResult = captcha.Validate(captcha.Code);
		Console.WriteLine($"Validate Correct Code: {correctResult}, IsUsed={captcha.IsUsed}, Attempts={captcha.Attempts}");

		// 4️⃣ Regenerate captcha
		var newCaptcha = await captchaService.RegenerateCaptchaAsync(captcha.Token);
		Console.WriteLine($"Old Captcha IsUsed: {captcha.IsUsed}, New Captcha Token: {newCaptcha.Token}, Code: {newCaptcha.Code}");

		// 5️⃣ Show if captcha required after failed attempts
		captchaService.RecordFailedAttempt("client1");
		captchaService.RecordFailedAttempt("client1");
		captchaService.RecordFailedAttempt("client1");

		bool showCaptcha = await captchaService.ShouldShowCaptchaAsync("client1");
		Console.WriteLine($"Should show captcha after 3 failed attempts? {showCaptcha}");

		Console.WriteLine("\n✅ Captcha Service Test Finished!");
	}
}
