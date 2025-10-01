using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.Repository.hotelInsurances
{
	public class hotelInsurancesUpdateDto
	{
		public int Id { get; set; }

		// آتش‌سوزی طبیعی
		public bool FireAndNaturalDisasterInsurance { get; set; } = false;

		// اموال تجهیزات
		public bool PropertyAndEquipmentInsurance { get; set; } = false;

		// درمان کارکنان
		public bool EmployeeSupplementaryHealthInsurance { get; set; } = false;

		// حوادث مهمانان
		public bool GuestAccidentInsurance { get; set; } = false;
	}
}
