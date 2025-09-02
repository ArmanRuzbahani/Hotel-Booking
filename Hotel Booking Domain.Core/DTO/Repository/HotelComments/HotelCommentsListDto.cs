using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.Repository.HotelComments
{
    public class HotelCommentsListDto
	{
		public int HotelId { get; set; }
		public string HotelName { get; set; }
		public List<HotelCommentReadDto> Comments { get; set; }
	}
}
