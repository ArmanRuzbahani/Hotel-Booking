using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Entitys.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using DrawingFont = System.Drawing.Font;

namespace Hotel_Booking_Application_Service.Services
{
	public class CaptchaService : ICaptchaService
	{
		private readonly List<Captcha> _captchas = new();
		private readonly Dictionary<string, int> _failedAttempts = new();
		private readonly object _lock = new();
		private readonly Random _random = new();

		public async Task<CaptchaResponse> GenerateCaptchaAsync()
		{
			var (code, imageData) = GenerateCaptchaImage();
			var captcha = new Captcha(code, imageData);

			lock (_lock)
			{
				_captchas.Add(captcha);
			}

			return new CaptchaResponse
			{
				Token = captcha.Token,
				ImageData = Convert.ToBase64String(captcha.ImageData),
				ExpiresAt = captcha.ExpiresAt,
				RemainingAttempts = captcha.GetRemainingAttempts()
			};
		}

		public async Task<ValidationResult> ValidateCaptchaAsync(string token, string code)
		{
			Captcha captcha;
			lock (_lock)
			{
				captcha = _captchas.FirstOrDefault(c => c.Token == token);
			}

			if (captcha == null)
				return new ValidationResult { ErrorMessage = "کد امنیتی معتبر نیست" };

			if (!captcha.CanAttempt())
				return new ValidationResult { IsExpired = true, ErrorMessage = "کد امنیتی منقضی شده" };

			var isValid = captcha.Validate(code);

			if (isValid)
			{
				return new ValidationResult { IsValid = true };
			}
			else
			{
				return new ValidationResult
				{
					IsValid = false,
					RemainingAttempts = captcha.GetRemainingAttempts(),
					ErrorMessage = captcha.GetRemainingAttempts() > 0
						? "کد امنیتی اشتباه است"
						: "تعداد تلاش‌ها تمام شد",
					RequiresNewCaptcha = captcha.GetRemainingAttempts() == 0
				};
			}
		}

		public async Task<bool> ShouldShowCaptchaAsync(string clientIdentifier)
		{
			lock (_lock)
			{
				if (_failedAttempts.TryGetValue(clientIdentifier, out var attempts))
					return attempts >= 3;
				return false;
			}
		}

		public async Task<CaptchaResponse> RegenerateCaptchaAsync(string oldToken)
		{
			lock (_lock)
			{
				for (int i = 0; i < _captchas.Count; i++)
				{
					if (_captchas[i].Token == oldToken)
					{
						_captchas[i].MarkAsUsed();
						break;
					}
				}
			}

			return await GenerateCaptchaAsync();
		}

		public void RecordFailedAttempt(string clientIdentifier)
		{
			lock (_lock)
			{
				if (_failedAttempts.ContainsKey(clientIdentifier))
					_failedAttempts[clientIdentifier]++;
				else
					_failedAttempts[clientIdentifier] = 1;
			}
		}

		public void ResetFailedAttempts(string clientIdentifier)
		{
			lock (_lock)
			{
				_failedAttempts.Remove(clientIdentifier);
			}
		}

		private (string code, byte[] imageData) GenerateCaptchaImage(int width = 200, int height = 80)
		{
			var code = GenerateRandomCode(6);
			var imageData = GenerateImage(code, width, height);
			return (code, imageData);
		}

		private string GenerateRandomCode(int length)
		{
			const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[_random.Next(s.Length)]).ToArray());
		}

		private byte[] GenerateImage(string code, int width, int height)
		{
			using var bitmap = new Bitmap(width, height);
			using var graphics = Graphics.FromImage(bitmap);
			using var stream = new MemoryStream();

			graphics.Clear(Color.White);
			DrawText(graphics, code, width, height);
			AddNoise(graphics, width, height);

			bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
			return stream.ToArray();
		}

		private void DrawText(Graphics graphics, string code, int width, int height)
		{
			var font = new DrawingFont("Arial", 12);
			var brush = new SolidBrush(Color.DarkBlue);
			var format = new StringFormat
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};

			graphics.DrawString(code, font, brush, new RectangleF(0, 0, width, height), format);
		}

		private void AddNoise(Graphics graphics, int width, int height)
		{
			for (int i = 0; i < 50; i++)
			{
				var pen = new Pen(Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256)), 1);
				graphics.DrawLine(pen,
					new Point(_random.Next(width), _random.Next(height)),
					new Point(_random.Next(width), _random.Next(height)));
			}
		}
	}
}
