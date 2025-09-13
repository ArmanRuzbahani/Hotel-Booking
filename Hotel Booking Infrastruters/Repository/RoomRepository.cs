using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Room;
using Hotel_Booking_Infrastruters.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Infrastruters.Repository
{
	public class RoomRepository : IRoomRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly ILogger<RoomRepository> _logger;

		public RoomRepository(AppDbContext appDbContext,ILogger<RoomRepository> logger){
			_appDbContext = appDbContext;
			_logger = logger;
			
			}
		public async Task<Room> CreateRoomAsync(RoomCreateDto roomCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				var room = new Room
				{
					Name = roomCreateDto.Name,
					Description = roomCreateDto.Description,
					roomType = roomCreateDto.RoomType,
					IsEmpty = roomCreateDto.IsEmpty,
					PricePerNight = roomCreateDto.PricePerNight,
					Discount = roomCreateDto.Discount,
					CountPriceAfterDiscount = roomCreateDto.PricePerNight - (roomCreateDto.PricePerNight * roomCreateDto.Discount / 100),
					HotelId = roomCreateDto.HotelId
				};

				await _appDbContext.rooms.AddAsync(room, cancellationToken);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return room;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating room");
				throw;
			}
		}

		public async Task<bool> DeleteRoomAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				var room = await _appDbContext.rooms
					.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

				if (room == null)
					return false;

				_appDbContext.rooms.Remove(room);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while deleting room");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Room>> GetAllRoomsAsync(CancellationToken cancellationToken)
		{
			try
			{
				var rooms = await _appDbContext.rooms
					.Include(r => r.Hotel)
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return rooms;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving all rooms");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Room>> GetRoomsByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				var rooms = await _appDbContext.rooms
					.Where(r => r.HotelId == hotelId)
					.Include(r => r.Hotel)
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return rooms;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving rooms by hotel ID");
				throw;
			}
		}


		public async Task<Room> UpdateRoomAsync(RoomUpdateDto roomUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				var room = await _appDbContext.rooms
					.FirstOrDefaultAsync(r => r.Id == roomUpdateDto.Id, cancellationToken);

				if (room == null)
					return null;

				room.Name = roomUpdateDto.Name;
				room.Description = roomUpdateDto.Description;
				room.roomType = roomUpdateDto.RoomType;
				room.IsEmpty = roomUpdateDto.IsEmpty;
				room.PricePerNight = roomUpdateDto.PricePerNight;
				room.Discount = roomUpdateDto.Discount;
				room.CountPriceAfterDiscount = roomUpdateDto.PricePerNight - (roomUpdateDto.PricePerNight * roomUpdateDto.Discount / 100);
				

				_appDbContext.rooms.Update(room);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return room;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while updating room");
				throw;
			}
		}
	}
}
