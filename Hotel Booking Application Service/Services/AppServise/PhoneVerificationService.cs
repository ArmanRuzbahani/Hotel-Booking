using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Entitys.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services.AppServise
{
	public class PhoneVerificationService : IPhoneVerificationService
	{
		private const string SessionKey = "PhoneVerificationCode";
		private const int CodeLength = 6; 
		private readonly HttpClient _httpClient;
		private const string SmsApiKey = "Ys6nWD1iv9KWLbHmZ7Gydbkl2Dg2K8Gh5TUiPsHmsv7HkoSf";
		private const int TemplateId = 100000;

		public PhoneVerificationService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.DefaultRequestHeaders.Add("x-api-key", SmsApiKey);
		}
		public async Task<string> GenerateAndSendCodeAsync(HttpContext httpContext, string phoneNumber)
		{
			
			var code = new Random().Next((int)Math.Pow(10, CodeLength - 1), (int)Math.Pow(10, CodeLength) - 1).ToString();

			
			httpContext.Session.SetString(SessionKey, code);

			
			var model = new VerifySendModel
			{
				Mobile = phoneNumber,
				TemplateId = TemplateId,
				Parameters = new[]
				{
				new VerifySendParameterModel { Name = "CODE", Value = code }
			}
			};

			var payload = JsonSerializer.Serialize(model);
			var stringContent = new StringContent(payload, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("https://api.sms.ir/v1/send/verify", stringContent);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception("ارسال پیامک موفقیت‌آمیز نبود.");
			}

			return code;
		}

		public async Task<bool> ValidateCodeAsync(HttpContext httpContext, string userInput)
		{
			return await Task.Run(() =>
			{
				var storedCode = httpContext.Session.GetString(SessionKey);
				if (string.IsNullOrEmpty(storedCode))
					return false;

				return storedCode == userInput.Trim();
			});
		}
	}
}
