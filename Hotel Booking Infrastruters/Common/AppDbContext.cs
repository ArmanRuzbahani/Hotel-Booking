using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Entitys;
using Hotel_Booking_Infrastruters.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Infrastruters.Common
{
	public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=ARMAN\SQLEXPRESS;Database=HotelBooking;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=True;");
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Booking>().HasOne(c => c.Hotel).WithMany(c => c.Bookings).HasForeignKey(c => c.HotelId).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<Booking>().HasOne(c => c.Room).WithMany(c => c.Bookings).HasForeignKey(c => c.RoomId).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<Booking>().HasOne(c => c.Customer).WithMany(c => c.Bookings).HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<Address>().HasOne(c => c.Customer).WithMany(c => c.Address).HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<HotelComments>().HasOne(c => c.Hotel).WithMany(c => c.HotelComments).HasForeignKey(c  => c.HotelId).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<HotelComments>().HasOne(c => c.Customer).WithMany(c => c.HotelComments).HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<HotelAddress>().HasOne(c => c.Hotel).WithMany(c => c.HotelAddresses).HasForeignKey(c => c.HotelId).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<Hotel>().HasOne(c => c.HotelManager).WithMany(c => c.hotels).HasForeignKey(c => c.HotelManagerId).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<HotelRules>().HasOne(c => c.Hotel).WithMany(c => c.HotelRules).HasForeignKey(c => c.HotelId).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<Room>().HasOne(c => c.Hotel).WithMany(c => c.rooms).HasForeignKey(c => c.HotelId).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<Facility>().HasMany(c => c.Hotel).WithMany(c => c.Facilities).UsingEntity(l => l.ToTable("HotelFacilities"));
			modelBuilder.Entity<HotelFood>().HasKey(c => c.Id);
			modelBuilder.Entity<HotelFood>().HasOne(c => c.Hotel).WithMany(c => c.hotelFoods).HasForeignKey(c => c.HotelId).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<HotelFood>().HasOne(c => c.Food).WithMany(c => c.HotelFoods).HasForeignKey(c => c.FoodId).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<Customer>().HasMany(c => c.Conversation).WithOne(c => c.Customer).HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<Hotel>().HasOne(h => h.hotelInsurances).WithOne(i => i.hotel).HasForeignKey<hotelInsurances>(i => i.Id).OnDelete(DeleteBehavior.Cascade);

			modelBuilder.ApplyConfiguration(new FoodConfiguration());
			modelBuilder.ApplyConfiguration(new FacilityConfiguration());
			//modelBuilder.ApplyConfiguration(new AdminConfiguration());
			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Address> addresses { get; set; }
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
		public DbSet<ChatConversation> chatConversations { get; set; }
	}
}
