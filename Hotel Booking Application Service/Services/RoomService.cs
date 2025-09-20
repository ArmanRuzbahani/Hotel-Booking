using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Room;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class RoomService : IRoomService
	{
		private readonly IRoomRepository _roomRepository;
		private readonly ILogger<RoomService> _logger;

		public RoomService(IRoomRepository roomRepository, ILogger<RoomService> logger)
		{
			_roomRepository = roomRepository;
			_logger = logger;
		}

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
