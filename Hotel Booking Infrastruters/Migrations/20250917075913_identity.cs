using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hotel_Booking_Infrastruters.Migrations
{
    /// <inheritdoc />
    public partial class identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facility",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facility", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmailVerfied = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPhoneNumberVerfied = table.Column<bool>(type: "bit", nullable: false),
                    CardId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Job = table.Column<int>(type: "int", nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Education = table.Column<int>(type: "int", nullable: true),
                    Nationality = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<int>(type: "int", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_admins_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmailVerfied = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPhoneNumberVerfied = table.Column<bool>(type: "bit", nullable: false),
                    CardId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Job = table.Column<int>(type: "int", nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Education = table.Column<int>(type: "int", nullable: true),
                    Nationality = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<int>(type: "int", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hotelManager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmailVerfied = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPhoneNumberVerfied = table.Column<bool>(type: "bit", nullable: false),
                    CardId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Job = table.Column<int>(type: "int", nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Education = table.Column<int>(type: "int", nullable: true),
                    Nationality = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<int>(type: "int", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotelManager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hotelManager_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_addresses_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "chatConversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConversationData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatConversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chatConversations_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenAt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CloseAt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    iranCityForHotel = table.Column<int>(type: "int", nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    HotelManagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hotels_hotelManager_HotelManagerId",
                        column: x => x.HotelManagerId,
                        principalTable: "hotelManager",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "hotelAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotelAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hotelAddresses_hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "hotels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "hotelComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotelComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hotelComments_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_hotelComments_hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "hotels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HotelFacilities",
                columns: table => new
                {
                    FacilitiesId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelFacilities", x => new { x.FacilitiesId, x.HotelId });
                    table.ForeignKey(
                        name: "FK_HotelFacilities_Facility_FacilitiesId",
                        column: x => x.FacilitiesId,
                        principalTable: "Facility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelFacilities_hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hotelFood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailableInYourHotel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotelFood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hotelFood_food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "food",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_hotelFood_hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "hotels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "hotelRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotelRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hotelRules_hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "hotels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    roomType = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    IsEmpty = table.Column<bool>(type: "bit", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    CountPriceAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rooms_hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "hotels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfNights = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bookings_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_bookings_hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "hotels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_bookings_rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Facility",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "پارکینگ" },
                    { 2, "استخر" },
                    { 3, "اینترنت رایگان" },
                    { 4, "رستوران" },
                    { 5, "خشکشویی" },
                    { 6, "سالن ورزشی" },
                    { 7, "اتاق جلسات" },
                    { 8, "اسپا و ماساژ" },
                    { 9, "خدمات اتاق" },
                    { 10, "تلویزیون کابلی" },
                    { 11, "مینی‌بار" },
                    { 12, "سرویس فرودگاهی" },
                    { 13, "تسهیلات دسترسی برای افراد معلول" },
                    { 14, "خدمات 24 ساعته" },
                    { 15, "بار و کافی‌شاپ" },
                    { 16, "اتاق‌های ضد سیگار" },
                    { 17, "خدمات تور و گردشگری" },
                    { 18, "پارک بازی کودکان" },
                    { 19, "آسانسور" },
                    { 20, "کتابخانه" }
                });

            migrationBuilder.InsertData(
                table: "food",
                columns: new[] { "Id", "Description", "Name", "Picture" },
                values: new object[,]
                {
                    { 1, "برنج ایرانی معطر همراه با ۲ سیخ کباب کوبیده گوسفندی و مخلفات", "چلو کباب", "koobide.jpg" },
                    { 2, "خورش اصیل ایرانی با سبزی تازه، لوبیا قرمز و گوشت گوسفندی", "قورمه سبزی", "qormesabzi.jpg" },
                    { 3, "خورش سنتی با گوشت قلقلی، گردوی آسیاب شده و رب انار ترش و شیرین", "فسنجان", "fesenjan-goosht-ghelgheli.jpg" },
                    { 4, "آبگوشت سنتی ایرانی با نخود، لوبیا، سیب‌زمینی و گوشت گوسفندی", "دیزی", "Abgoosht.jpg" },
                    { 5, "خورش لذیذ با لپه، سیب‌زمینی سرخ‌شده و گوشت گوسفندی", "قیمه", "qeyme.jpg" },
                    { 6, "برنج زعفرانی همراه با مرغ سرخ‌شده و زرشک تازه", "زرشک پلو با مرغ", "zereshk-polo-morgh.jpg" },
                    { 7, "پلو معطر با شوید و باقالی به همراه گوشت ماهیچه گوسفندی", "باقالی پلو با ماهیچه", "baghali-polo-ba-mahicheh.jpg" },
                    { 8, "لایه‌های برنج زعفرانی با مرغ، ماست و زرده تخم‌مرغ، با ته‌دیگ طلایی", "ته چین", "Tachin.jpg" },
                    { 9, "فیله مرغ مزه‌دارشده و کبابی با برنج ایرانی و گوجه کبابی", "جوجه کباب", "jojekabab.jpg" },
                    { 10, "برنج ایرانی همراه با رشته پلویی، کشمش و گوشت خورشتی", "رشته پلو", "reshteh-polo-gosht-min.jpg" },
                    { 11, "ترکیب بادمجان سرخ‌شده، کشک، نعناع داغ و گردوی خرد شده", "کشک بادمجان", "kashk-bademjan-kababi.jpg" },
                    { 12, "مرغ پخته‌شده با ادویه‌های مخصوص هندی و برنج معطر", "مرغ بریانی", "beryani.jpg" },
                    { 13, "پلو با شوید و باقالی تازه همراه با مرغ سرخ‌شده", "باقالی پلو با مرغ", "baghali-polo-morgh-1.jpg" },
                    { 14, "پیتزا با گوشت چرخ‌کرده، قارچ، فلفل دلمه‌ای و پنیر کشدار", "پیتزا مخلوط", "makhloot.jpg" },
                    { 15, "پیتزا با مرغ، قارچ، گوشت دودی و پنیر موزارلا", "پیتزای مخصوص", "makhsoos.jpg" },
                    { 16, "پیتزای کلاسیک با پپرونی تند، پنیر و سس گوجه‌فرنگی", "پیتزای پپرونی", "pizzapeperoni.jpg" },
                    { 17, "پیتزا با تکه‌های استیک گوشت، سیر تازه و پنیر کشدار", "پیتزای سیر استیک", "pizzasirestake.jpg" },
                    { 18, "مارگاریتا با گوجه تازه، ریحان سبز و پنیر موزارلا", "پیتزای ایتالیایی", "pizzaitaly.jpg" },
                    { 19, "پیتزا گیاهی با قارچ، ذرت، زیتون، فلفل دلمه‌ای و پنیر", "پیتزا سبزیجات", "pizzasabsijat.jpg" },
                    { 20, "پیتزا با گوشت چرخ‌کرده، سس مخصوص و پنیر زیاد", "پیتزای آمریکایی", "pizzaamerican.jpg" },
                    { 21, "پیتزا ترکی با سوسیس سوجوک، سبزیجات و پنیر", "پیتزای سوجوک", "pizaasojock.jpeg" },
                    { 22, "پاستا با سس آلفردو، مرغ گریل‌شده و قارچ تازه", "پاستا آلفردو", "pastaalfredo.jpg" },
                    { 23, "لازانیا با گوشت چرخ‌کرده، سس بشامل و پنیر فراوان", "لازانیا گوشت", "lazania.jpg" },
                    { 24, "مرغ خلالی با قارچ، پیاز و سس خامه‌ای", "چیکن استراگانوف", "chiken-estraganof.jpg" },
                    { 25, "مرغ ترد سوخاری‌شده با ادویه مخصوص و سیب‌زمینی سرخ‌کرده", "مرغ سوخاری", "kentaki.jpg" },
                    { 26, "سالاد سبزیجات تازه با سس مخصوص", "سالاد", "salad.jpg" },
                    { 27, "کاهو تازه، مرغ گریل‌شده، پنیر پارمسان و نان برشته", "سالاد سزار", "sezar.jpg" },
                    { 29, "آب معدنی خنک و تازه", "آب", "water.png" },
                    { 30, "نوشابه گازدار با طعم کولا", "نوشابه مشکی", "kok.jpg" },
                    { 31, "نوشابه گازدار با طعم پرتقال", "نوشابه زرد", "fanta.jpg" },
                    { 32, "نوشابه گازدار با طعم لیمو", "اسپرایت", "sprite.jpg" },
                    { 33, "دوغ سنتی ایرانی با نعناع", "دوغ", "doq.jpg" },
                    { 34, "پلو ایرانی با لوبیا سبز، گوشت و رب گوجه", "لوبیا پلو", "loobia-polo.jpg" },
                    { 35, "پاستای ایرانی با سس گوشت و رب گوجه", "ماکارونی", "makarani.jpg" },
                    { 36, "دلمه برگ مو با برنج، سبزیجات و گوشت چرخ‌کرده", "دلمه", "dolme-barg-mo.jpg" },
                    { 37, "پلو سبزی‌جات معطر همراه با ماهی سرخ‌شده یا کبابی", "سبزی پلو با ماهی", "sabzi-polo-ba-mahi.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_addresses_CustomerId",
                table: "addresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_admins_AppUserId",
                table: "admins",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_CustomerId",
                table: "bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_HotelId",
                table: "bookings",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_RoomId",
                table: "bookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_chatConversations_CustomerId",
                table: "chatConversations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_customers_AppUserId",
                table: "customers",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_hotelAddresses_HotelId",
                table: "hotelAddresses",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_hotelComments_CustomerId",
                table: "hotelComments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_hotelComments_HotelId",
                table: "hotelComments",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelFacilities_HotelId",
                table: "HotelFacilities",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_hotelFood_FoodId",
                table: "hotelFood",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_hotelFood_HotelId",
                table: "hotelFood",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_hotelManager_AppUserId",
                table: "hotelManager",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_hotelRules_HotelId",
                table: "hotelRules",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_hotels_HotelManagerId",
                table: "hotels",
                column: "HotelManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_HotelId",
                table: "rooms",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "chatConversations");

            migrationBuilder.DropTable(
                name: "hotelAddresses");

            migrationBuilder.DropTable(
                name: "hotelComments");

            migrationBuilder.DropTable(
                name: "HotelFacilities");

            migrationBuilder.DropTable(
                name: "hotelFood");

            migrationBuilder.DropTable(
                name: "hotelRules");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "Facility");

            migrationBuilder.DropTable(
                name: "food");

            migrationBuilder.DropTable(
                name: "hotels");

            migrationBuilder.DropTable(
                name: "hotelManager");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
