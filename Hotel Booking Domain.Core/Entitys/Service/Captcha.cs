using System;

namespace Hotel_Booking_Domain.Core.Entitys.Service
{
	public class Captcha
	{
		public string Id { get; private set; }
		public string Token { get; private set; }
		public string Code { get; private set; }
		public DateTime ExpiresAt { get; private set; }
		public bool IsUsed { get; private set; }
		public int Attempts { get; private set; }
		public byte[] ImageData { get; private set; }

		public Captcha(string code, byte[] imageData, int expirationMinutes = 5)
		{
			Id = Guid.NewGuid().ToString();
			Token = Guid.NewGuid().ToString();
			Code = code;
			ImageData = imageData;
			ExpiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);
			IsUsed = false;
			Attempts = 0;
		}

		public bool Validate(string inputCode)
		{
			if (IsUsed || IsExpired()) return false;

			Attempts++;
			var isValid = string.Equals(Code, inputCode, StringComparison.OrdinalIgnoreCase);

			if (isValid) IsUsed = true;
			return isValid;
		}

		public void MarkAsUsed()
		{
			IsUsed = true;
		}

		public bool IsExpired() => DateTime.UtcNow > ExpiresAt;
		public bool CanAttempt() => !IsUsed && !IsExpired() && Attempts < 3;
		public int GetRemainingAttempts() => Math.Max(0, 3 - Attempts);
	}
}
