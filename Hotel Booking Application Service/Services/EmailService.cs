using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class EmailService : IEmailService
	{
		private readonly IMemoryCache _memoryCache;
		private readonly HttpClient _httpClient;
		private readonly string _apiKey = "e46a2c10-d465-480a-88ec-6b19f126603d";
		private readonly string _apiUrl = "https://api.brevo.com/v3/smtp/email";

		public EmailService(IMemoryCache memoryCache, HttpClient httpClient)
		{
			_memoryCache = memoryCache;
			_httpClient = httpClient;
			_httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
		}

		public async Task SendOtpAsync(string email, string otpCode)
		{
			var emailRequest = new
			{
				sender = new { name = "Your App", email = "noreply@yourapp.com" },
				to = new[] { new { email = email, name = email } },
				subject = "Your OTP Code",
				htmlContent = $"<h3>Your OTP code is: {otpCode}</h3><p>This code will expire in 5 minutes.</p>",
				textContent = $"Your OTP code is: {otpCode}"
			};

			var json = JsonSerializer.Serialize(emailRequest);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync(_apiUrl, content);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"Failed to send email: {response.StatusCode}");
			}

			// Store OTP in cache
			var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
			_memoryCache.Set(email, otpCode, cacheOptions);
		}

		public async Task<bool> VerifyEmailAsync(string email, string otpCode)
		{
			if (_memoryCache.TryGetValue(email, out string storedOtp))
			{
				if (storedOtp == otpCode)
				{
					_memoryCache.Remove(email);
					return true;
				}
			}
			return false;
		}
	}
}