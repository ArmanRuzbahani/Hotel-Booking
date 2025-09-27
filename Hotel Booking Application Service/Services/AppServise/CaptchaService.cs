using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Entitys.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services.AppServise
{
	public class CaptchaService : ICaptchaService
	{
		private const string SessionKey = "CaptchaAnswer";

		public async Task<string> GenerateCaptchaAsync(HttpContext httpContext)
		{
			var newCaptcha = Captcha.GenerateMathCaptcha();
			httpContext.Session.SetString(SessionKey, newCaptcha.Answer.ToString());
			return await Task.FromResult(newCaptcha.Question);
		}

		public async Task<bool> ValidateCaptchaAsync(HttpContext httpContext, string userResponse)
		{
			var answerString = httpContext.Session.GetString(SessionKey);

			if (string.IsNullOrEmpty(answerString) || !int.TryParse(answerString, out int answer) || !int.TryParse(userResponse, out int response))
			{
				return await Task.FromResult(false);
			}

			return await Task.FromResult(response == answer);
		}
	}
}
