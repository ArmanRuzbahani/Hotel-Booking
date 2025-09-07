using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.DTO.Repository.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Repository
{
	public interface IRoomRepository
	{
		Task<IReadOnlyCollection<Room>> GetAllRoomsAsync(CancellationToken cancellationToken);

		Task<IReadOnlyCollection<Room>> GetRoomsByHotelIdAsync(int HotelId, CancellationToken cancellationToken);

		Task<Room> CreateRoomAsync(RoomCreateDto roomCreateDto, CancellationToken cancellationToken);

		Task<Room> UpdateRoomAsync(RoomUpdateDto roomUpdateDto, CancellationToken cancellationToken);

		Task<bool> DeleteRoomAsync(int id, CancellationToken cancellationToken);

	}
}
