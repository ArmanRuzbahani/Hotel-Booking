using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Application_Service
{
	public interface ICaptchaService
	{
		Task<string> GenerateCaptchaAsync(HttpContext httpContext);

		Task<bool> ValidateCaptchaAsync(HttpContext httpContext, string userResponse);
	}
}
