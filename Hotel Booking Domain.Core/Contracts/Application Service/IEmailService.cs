using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Application_Service
{
	public interface IEmailService
	{
		Task SendOtpAsync(string email, string otpCode);

		Task<bool> VerifyEmailAsync(string email, string otpCode);
	}
}
