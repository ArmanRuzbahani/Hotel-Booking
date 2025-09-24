using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Room;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

		public async Task<Room> CreateRoomAsync(RoomCreateDto roomCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (roomCreateDto == null)
				{
					_logger.LogError("ایجاد اتاق ناموفق بود: داده ورودی خالی است");
					throw new ArgumentNullException(nameof(roomCreateDto));
				}

				if (roomCreateDto.HotelId <= 0)
				{
					_logger.LogError("ایجاد اتاق ناموفق بود: شناسه هتل نامعتبر است");
					throw new ArgumentException("شناسه هتل نامعتبر است");
				}

				if (roomCreateDto.PricePerNight < 0)
				{
					_logger.LogError("ایجاد اتاق ناموفق بود: قیمت هر شب نامعتبر است");
					throw new ArgumentException("قیمت هر شب نمی‌تواند منفی باشد");
				}

				if (roomCreateDto.Discount < 0 || roomCreateDto.Discount > 100)
				{
					_logger.LogError("ایجاد اتاق ناموفق بود: تخفیف نامعتبر است");
					throw new ArgumentException("تخفیف باید بین 0 تا 100 باشد");
				}

				var room = await _roomRepository.CreateRoomAsync(roomCreateDto, cancellationToken);
				if (room == null)
				{
					_logger.LogError("ایجاد اتاق ناموفق بود: خطا در ذخیره‌سازی");
					throw new InvalidOperationException("خطا در ایجاد اتاق");
				}

				_logger.LogInformation("اتاق با نام {RoomName} با موفقیت ایجاد شد", room.Name);
				return room;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در ایجاد اتاق با نام: {RoomName}", roomCreateDto.Name);
				throw;
			}
		}

		public async Task<bool> DeleteRoomAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				if (id <= 0)
				{
					_logger.LogError("حذف اتاق ناموفق بود: شناسه نامعتبر است");
					throw new ArgumentException("شناسه اتاق نامعتبر است");
				}

				var result = await _roomRepository.DeleteRoomAsync(id, cancellationToken);
				if (!result)
				{
					_logger.LogWarning("اتاق با شناسه {RoomId} یافت نشد", id);
					return false;
				}

				_logger.LogInformation("اتاق با شناسه {RoomId} با موفقیت حذف شد", id);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در حذف اتاق با شناسه: {RoomId}", id);
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Room>> GetAllRoomsAsync(CancellationToken cancellationToken)
		{
			try
			{
				var rooms = await _roomRepository.GetAllRoomsAsync(cancellationToken);
				if (rooms == null || rooms.Count == 0)
				{
					_logger.LogWarning("هیچ اتاقی یافت نشد");
					return new List<Room>();
				}

				_logger.LogInformation("تعداد {RoomCount} اتاق با موفقیت بازیابی شد", rooms.Count);
				return rooms;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در بازیابی لیست اتاق‌ها");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Room>> GetRoomsByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelId <= 0)
				{
					_logger.LogError("بازیابی اتاق‌ها ناموفق بود: شناسه هتل نامعتبر است");
					throw new ArgumentException("شناسه هتل نامعتبر است");
				}

				var rooms = await _roomRepository.GetRoomsByHotelIdAsync(hotelId, cancellationToken);
				if (rooms == null || rooms.Count == 0)
				{
					_logger.LogWarning("هیچ اتاقی برای هتل با شناسه {HotelId} یافت نشد", hotelId);
					return new List<Room>();
				}

				_logger.LogInformation("تعداد {RoomCount} اتاق برای هتل با شناسه {HotelId} بازیابی شد", rooms.Count, hotelId);
				return rooms;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در بازیابی اتاق‌ها برای هتل با شناسه: {HotelId}", hotelId);
				throw;
			}
		}

		public async Task<Room> UpdateRoomAsync(RoomUpdateDto roomUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (roomUpdateDto == null || roomUpdateDto.Id <= 0)
				{
					_logger.LogError("به‌روزرسانی اتاق ناموفق بود: داده ورودی یا شناسه نامعتبر است");
					throw new ArgumentException("داده ورودی یا شناسه نامعتبر است");
				}

				if (roomUpdateDto.PricePerNight < 0)
				{
					_logger.LogError("به‌روزرسانی اتاق ناموفق بود: قیمت هر شب نامعتبر است");
					throw new ArgumentException("قیمت هر شب نمی‌تواند منفی باشد");
				}

				if (roomUpdateDto.Discount < 0 || roomUpdateDto.Discount > 100)
				{
					_logger.LogError("به‌روزرسانی اتاق ناموفق بود: تخفیف نامعتبر است");
					throw new ArgumentException("تخفیف باید بین 0 تا 100 باشد");
				}

				var room = await _roomRepository.UpdateRoomAsync(roomUpdateDto, cancellationToken);
				if (room == null)
				{
					_logger.LogWarning("اتاق با شناسه {RoomId} برای به‌روزرسانی یافت نشد", roomUpdateDto.Id);
					throw new InvalidOperationException("اتاق یافت نشد");
				}

				_logger.LogInformation("اتاق با شناسه {RoomId} با موفقیت به‌روزرسانی شد", roomUpdateDto.Id);
				return room;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در به‌روزرسانی اتاق با شناسه: {RoomId}", roomUpdateDto.Id);
				throw;
			}
		}
	}
}