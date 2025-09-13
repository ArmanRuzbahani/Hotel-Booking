using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Infrastruters.Repository
{
	public class RoomRepository : IRoomRepository
	{
		public Task<Room> CreateRoomAsync(RoomCreateDto roomCreateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteRoomAsync(int id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<Room>> GetAllRoomsAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<Room>> GetRoomsByHotelIdAsync(int HotelId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<Room> UpdateRoomAsync(RoomUpdateDto roomUpdateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
