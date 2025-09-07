# 🏨 Hotel Booking System

A modern hotel booking system built with **.NET 9**, **Angular**, and **Onion Architecture**. The system includes advanced features like email & phone verification, captcha, chatbot integration, caching, logging, and more.

---

## **🛠 Technology Stack**

- **Backend:** .NET 9, ASP.NET Core MVC, Onion Architecture  
- **Frontend:** Angular (Web Framework)  
- **Database:** SQL Server, Entity Framework Core  
- **Caching:** Redis  
- **Logging:** In-Memory Logging  
- **Authentication & Verification:** Email and Phone Number Verification  
- **Security:** Captcha Service  
- **Additional Features:** Chatbot Integration  
- **Future Enhancements:** Reporting and Analytics

---

## **📂 Project Architecture (Onion Structure)**

### **1. Core (Domain Layer)**
- Entities: `User`, `Hotel`, `HotelRoom`, `Reservation`, `HotelComment`, `Address`, `Food`  
- Value Objects: `Email`, `PhoneNumber`, `Rating`, `BookingDate`  
- Interfaces for Repositories & Services

### **2. Application Layer**
- DTOs: `UserDto`, `ReservationDto`, `HotelDto`, etc.  
- Services: `BookingService`, `UserService`, `EmailService`, `CaptchaService`  
- Business Rules & Validations  

### **3. Infrastructure Layer**
- EF Core / DbContext Implementation  
- Redis Caching Implementation  
- Email & SMS Providers  
- Logging Mechanism  

### **4. Web (Presentation Layer)**
- ASP.NET Core MVC Controllers  
- Angular Components & Services  
- User Authentication & Authorization  

---

## **⚡ Features**

- **User Management:** Register, Login, Email & Phone Verification  
- **Hotel Management:** Add, Edit, View Hotels and Rooms  
- **Booking Management:** Reserve Rooms, Cancel Bookings  
- **Comment & Rating System:** Hotel Reviews  
- **Security Features:** Captcha Verification  
- **Chatbot:** Automated Assistance  
- **Caching:** Redis for fast data retrieval  
- **Logging:** In-Memory logging for monitoring  
- **Future Features:** Advanced Reporting & Analytics  

---
