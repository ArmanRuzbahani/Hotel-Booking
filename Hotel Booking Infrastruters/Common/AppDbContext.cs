using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Infrastruters.Common
{
	public class AppDbContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=ARMAN\SQLEXPRESS;Database=HotelBooking;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=True;");

			base.OnConfiguring(optionsBuilder);
		}


		public DbSet<Address> adresses { get; set; }
		public DbSet<Admin> admins { get; set; }
		public DbSet<Booking> bookings { get; set; }
		public DbSet<Customer> customers { get; set; }
		public DbSet<Hotel> hotels { get; set; }
		public DbSet<HotelAddress> hotelAddresses { get; set; }
		public DbSet<HotelComments> hotelComments { get; set; }
		public DbSet<HotelManager> hotelManager { get; set; }
		public DbSet<Room> rooms { get; set; }
		public DbSet<Facility> Facility { get; set; }
		public DbSet<Food> food { get; set; }
		public DbSet<HotelRules> hotelRules { get; set; }
		public DbSet<HotelFood> hotelFood { get; set; }


	}
}
