using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Application_Service
{
	public interface ICaptchaService
	{
		Task<CaptchaResponse> GenerateCaptchaAsync();
		Task<ValidationResult> ValidateCaptchaAsync(string token, string code);
		Task<bool> ShouldShowCaptchaAsync(string clientIdentifier);
		Task<CaptchaResponse> RegenerateCaptchaAsync(string oldToken);
	}

	public class CaptchaResponse
	{
		public string Token { get; set; }
		public string ImageData { get; set; } // Base64
		public DateTime ExpiresAt { get; set; }
		public int RemainingAttempts { get; set; }
	}

	public class ValidationResult
	{
		public bool IsValid { get; set; }
		public bool IsExpired { get; set; }
		public int RemainingAttempts { get; set; }
		public string ErrorMessage { get; set; }
		public bool RequiresNewCaptcha { get; set; }
	}
}
