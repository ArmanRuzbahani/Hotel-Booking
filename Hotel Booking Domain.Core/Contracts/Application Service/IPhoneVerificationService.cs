using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Application_Service
{
	public interface IPhoneVerificationService
	{
		Task<string> GenerateAndSendCodeAsync(HttpContext httpContext, string phoneNumber);
		Task<bool> ValidateCodeAsync(HttpContext httpContext, string userInput);
	}
}
