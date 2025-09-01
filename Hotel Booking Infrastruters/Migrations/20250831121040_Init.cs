using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hotel_Booking_Infrastruters.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Job = table.Column<int>(type: "int", nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Education = table.Column<int>(type: "int", nullable: true),
                    Nationality = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<int>(type: "int", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Job = table.Column<int>(type: "int", nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Education = table.Column<int>(type: "int", nullable: true),
                    Nationality = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<int>(type: "int", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
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
                name: "HotelManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Job = table.Column<int>(type: "int", nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Education = table.Column<int>(type: "int", nullable: true),
                    Nationality = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<int>(type: "int", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdressName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_addresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
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
                        name: "FK_hotels_HotelManagers_HotelManagerId",
                        column: x => x.HotelManagerId,
                        principalTable: "HotelManagers",
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
                        name: "FK_hotelComments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
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
                        name: "FK_bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
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
                table: "Admins",
                columns: new[] { "Id", "CardId", "City", "DateOfBirth", "Education", "Email", "Gender", "IsActive", "Job", "LastName", "MaritalStatus", "Name", "Nationality", "PhoneNumber", "Role", "UserCreateAt" },
                values: new object[] { 1, "1234567890123456", 1, new DateTime(2005, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "arman.ruzb@gmail.com", 1, true, 3, "روزبهانی", 2, "آرمان", 1, "+989123456789", 0, new DateTime(2025, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) });

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
                    { 1, "با برنج ایرانی مخلفات و 200 گرم گوشت گوسفندی", "چلو کباب", "" },
                    { 2, "خورشتی ایرانی با سبزیجات، گوشت و لوبیا", "قورمه سبزی", "" },
                    { 3, "خورشت با گوشت و سس گردو و رب انار", "فسنجان", "" },
                    { 4, "خوراک سنتی ایرانی با گوشت گوسفند و سبزیجات", "دیزی", "" },
                    { 5, "برنج ایرانی مخلفات", "قیمه", "" },
                    { 6, "برنج زعفرانی همراه با مرغ و زرشک", "زرشک پلو با مرغ", "" },
                    { 7, "پلو با باقالی و گوشت ماهیچه", "باقالی پلو با ماهیچه", "" },
                    { 8, "پلو با مرغ و زعفران و ماست", "ته چین", "" },
                    { 9, "برنج ایرانی 200 گرم جوجه سینه", "جوجه کباب", "" },
                    { 10, "پلو با رشته، گوشت و زعفران", "رشته پلو", "" },
                    { 11, "غذای ساده و خوشمزه با بادمجان و کشک", "کشک بادمجان", "" },
                    { 12, "مرغ پخته شده با ادویه‌های خاص", "مرغ بریانی", "" },
                    { 13, "پلو با باقالی و گوشت", "باقالی پلو با مرغ", "" },
                    { 14, "پیتزا با ترکیب مختلف مواد اولیه مانند قارچ، گوشت، فلفل دلمه‌ای و پنیر", "پیتزا مخلوط", "" },
                    { 15, "پیتزا با ترکیب مواد خاص مانند گوشت مرغ، قارچ و پنیر موزارلا", "پیتزای مخصوص", "" },
                    { 16, "پیتزا با تکه‌های پپرونی، پنیر و سس گوجه‌فرنگی", "پیتزای پپرونی", "" },
                    { 17, "پیتزا با گوشت استیک، سیر و پنیر", "پیتزای سیر استیک", "" },
                    { 18, "پیتزای کلاسیک ایتالیایی با ترکیب ریحان تازه، گوجه‌فرنگی، پنیر موزارلا و روغن زیتون", "پیتزای ایتالیایی", "" },
                    { 19, "پیتزا با انواع سبزیجات تازه مانند قارچ، فلفل دلمه‌ای، زیتون و پنیر", "پیتزا سبزیجات", "" },
                    { 20, "پیتزا با گوشت چرخ کرده، پنیر موزارلا، فلفل دلمه‌ای و سس گوجه‌فرنگی", "پیتزای امریکایی", "" },
                    { 21, "پیتزا با سوجوک (سوسیس ترکی) به همراه پنیر و سبزیجات", "پیتزای سوجوک", "" },
                    { 22, "پیتزا با سس آلفردو، مرغ پخته و پنیر", "پاستا الفردو", "" },
                    { 23, "لازانیا با لایه‌های نودل، گوشت چرخ کرده، پنیر و سس بشامل", "لازانیا گوشت", "" },
                    { 24, "مرغ پخته شده با سس خامه‌ای، پیاز و قارچ", "چیکن استراناگوف", "" },
                    { 25, "مرغ تکه‌ای پخته شده با طعم‌های مختلف و پوشش داغ و ترد از آرد و ادویه", "مرغ سخاری", "" },
                    { 26, "کاهو گوجه خیار سس مخصوص", "سالاد", "" },
                    { 27, "کاهو مرغ گریل شده", "سالاد سزار", "" },
                    { 29, "بزرگ", "آب", "" },
                    { 30, "بزرگ", "نوشابه مشکی", "" },
                    { 31, "بزرگ", "نوشابه زرد", "" },
                    { 32, "بزرگ", "اسپرایت", "" },
                    { 33, "بزرگ", "دوغ", "" },
                    { 34, "بزرگ", "آب گازدار", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_addresses_CustomerId",
                table: "addresses",
                column: "CustomerId");

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
                name: "Admins");

            migrationBuilder.DropTable(
                name: "bookings");

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
                name: "rooms");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Facility");

            migrationBuilder.DropTable(
                name: "food");

            migrationBuilder.DropTable(
                name: "hotels");

            migrationBuilder.DropTable(
                name: "HotelManagers");
        }
    }
}
