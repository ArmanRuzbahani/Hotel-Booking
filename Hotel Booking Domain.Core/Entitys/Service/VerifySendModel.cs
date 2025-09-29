using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Entitys.Service
{
	public class VerifySendModel
	{
		public string Mobile { get; set; }
		public int TemplateId { get; set; }
		public VerifySendParameterModel[] Parameters { get; set; }
	}
}
