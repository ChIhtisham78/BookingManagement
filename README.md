## Shaghaf Overview
Shaghaf is a comprehensive application designed for booking and renting various rooms for multiple purposes such as training, meetings, social events, and birthday parties. The application provides user-friendly interfaces to display rooms, book them, manage memberships,  and order additional services like ordering drinks and food. You can also conveniently book a photo session in any of our themed rooms.

## Technical Details
# Shaghaf API

Shaghaf API is a modular and scalable web application designed to manage bookings, orders, memberships, and more. The application is built using modern technologies and follows the Onion Architecture to ensure a clear separation of concerns, scalability, and maintainability.

## Technologies

- **.NET 7.0**
- **Entity Framework Core (EF Core)**
- **LINQ (Language Integrated Query)**
- **Stripe Payment Gateway**
- **AutoMapper**
- **RESTful APIs**
- **MS SQL Server**
- **JWT Authentication**
- **Identity for User Management**
- **Redis** (used for storing cart items)
- **Twilio SMS Integration** (for sending verification codes and password reset functionality)

## Architecture

Shaghaf API is structured using Onion Architecture, which focuses on the following layers:

* **Shaghaf.API**: Manages incoming HTTP requests and middleware.
* **Shaghaf.Core**: Houses the core business logic and domain models.
* **Shaghaf.Infrastructure**: Manages data access via EF Core, using patterns like Generic Repository and Unit of Work.
* **Shaghaf.Service**: Provides service layer functionalities.

## System Components

### 1. Rooms
Rooms in Shaghaf are versatile spaces available for a range of activities:
- **Training Rooms**: Equipped with educational tools like whiteboards and projectors.
- **Meeting Rooms**: Set up for corporate meetings with necessary conferencing equipment.
- **Event Rooms**: Ideal for hosting larger gatherings or social events.
- **Birthday Rooms**: Decorated spaces for celebrating birthdays, including party amenities.
- **Workspaces**: Co-working areas designed for professionals and freelancers.
- **Game Rooms**: Entertainment zones equipped with various gaming setups.
- **Conference Rooms**: Large areas suitable for conferences and large scale meetings.

**Attributes**:
- Name, Offer, Rate, Seat Capacity, Description, Location, Available Dates, Pricing Plans, and Room Type.

### 2. Birthdays
Shaghaf allows users to book rooms specifically for birthday events, which can be customized with various options:
- **User Name**: Who is hosting the event.
- **Date and Number of Guests**: Schedule and size of the party.
- **Cakes and Decorations**: Customizable options for cakes and decor.
- **Photo Sessions**: Optional photo sessions can be added.

### 3. Photo Sessions
Available as standalone bookings or part of event packages, photo sessions include:
- **Cost and Duration**: Pricing and time slots available.
- **Date and Location**: Scheduling and places (indoor/outdoor).
- **Associated Room or Event**: Linking to specific rooms or events.

### 4. Memberships
Memberships provide special privileges and discounts for frequent users:
- **Name and Price**: Type of membership and cost.
- **Features**: Benefits such as access to exclusive rooms and events.
- **Duration and Guest Allowance**: Validity period and guest limits.

### 5. Bookings
Central to Shaghaf’s operations, bookings can be made for rooms, services, or special events:
- **Details**: Room or event type, customer information, dates, and financials.
- **Management Features**: Options to view, update, or cancel bookings.

### 6. Account Management
Shaghaf also provides robust account management functionalities, allowing users to register, log in, and manage roles:
- **Register**: Users can create new accounts.
- **Login**: Account holders can log into the system.
- **Add Role**: Admins can assign roles to users, enhancing control over permissions and access.
- **SMS Verification**: Secure accounts using SMS-based verification for registration and password reset.
- **Reset Password**: Securely reset passwords using SMS verification.
- 
### 7. Home Data Management
The system allows administrators to manage various data points displayed on the home page, including advertisements and categories.

## Generic Repository and Unit of Work

Shaghaf API utilizes the Generic Repository pattern and the Unit of Work pattern to manage data access operations consistently and efficiently across different entities.

- **Generic Repository**: A reusable repository pattern that provides a consistent interface for CRUD operations. The `GenericRepository` class implements common data access operations, ensuring that all repositories adhere to the same standard and reducing code duplication.

- **Unit of Work**: The `UnitOfWork` pattern manages transactions across multiple repository operations, ensuring consistency and integrity. This pattern is crucial in scenarios where multiple changes need to be committed to the database as a single atomic operation, thereby preventing partial updates and maintaining data consistency.

The following specific repositories are implemented within the Shaghaf API:

- **BookingRepository**: Manages data access for booking-related entities.
- **CartRepository**: Handles CRUD operations for cart items, particularly leveraging Redis for efficient in-memory data storage.


## Controllers

- **AccountController**
- **BookingController**
- **CartController**
- **OrdersController**
- **MembershipController**
- **PhotoSessionController**
- **RoomController**
- **HomeController**


### **Entities**
- **Room**: Represents rooms available for booking, including various types and attributes.
- **Birthday**: Manages birthday event details, including cakes, decorations, and associated room bookings.
- **PhotoSession**: Handles photo session details, including cost, duration, and location.
- **Membership**: Represents different membership plans available to users, including features and benefits.
- **Booking**: Central entity for managing room and event bookings.
- **Order**: Handles the ordering process, including payment and cart management.
- **Cart**: Manages items added to the user's cart for ordering.
- **MenuItem**: Represents menu items available for order in the system.

### **Services**
- **AuthService**: Manages user authentication, registration, and role management.
- **BookingService**: Handles the booking process, including creation, updating, and payment processing.
- **OrderService**: Manages the ordering process, including payment, cart management, and order status checking.
- **MembershipService**: Handles the creation and management of memberships.
- **PhotoSessionService**: Manages the scheduling and details of photo sessions.
- **RoomService**: Handles the creation and management of rooms.

### **IServices (Interfaces)**
- **IAuthService**
- **IBookingService**
- **IOrderService**
- **IMembershipService**
- **IPhotoSessionService**
- **IRoomService**

## Redis Integration

### **Redis Database**
- Redis is used for caching data, particularly for managing shopping cart data. It provides fast, in-memory data storage, which improves the performance and scalability of the application.

### **Redis Services**
- **CartRepository**: Manages the operations related to the cart in Redis, including retrieval, updating, and deletion.
- **Redis Cache**: Provides caching for frequently accessed data, reducing database load and improving response times.

### **Redis IServices (Interfaces)**
- **ICartRepository**: Interface for managing cart operations in Redis.

## Mappings

AutoMapper is used to map between Data Transfer Objects (DTOs) and entities, ensuring that data is transformed correctly as it moves between different layers of the application.

## Specifications

Specifications are used to encapsulate complex query logic, allowing for flexible and maintainable data retrieval. Examples include:

- **BookingWithDetailsSpec**: Retrieves booking details along with associated orders.
- **MenuItemByCategorySpec**: Filters menu items by category.
- **RoomsWithMembershipsSpec**: Retrieves rooms along with their associated memberships.
Shaghaf API is a comprehensive web application designed to manage various business operations, including room bookings, memberships, orders, and more. The application adopts a robust Onion Architecture, utilizing modern technologies to ensure scalability, maintainability, and performance. Built with ASP.NET Core and Entity Framework Core, the API provides a secure, efficient, and modular backend system.


## DTOs (Data Transfer Objects)

DTOs play a crucial role in the Shaghaf API by ensuring that only the necessary data is passed between the client and the server. Here are the key DTOs used in the project:

- **BirthdayDtos**:
  - `BirthdayDto.cs`: Represents a birthday event with details like cakes, decorations, and room information.
  - `BirthdayToCreateDto.cs`: Used for creating new birthday events.

- **BookingDtos**:
  - `BookingDto.cs`: Represents the booking details, including room, birthday, and photo session information.
  - `BookingToCreateDto.cs`: Used for creating new bookings.

- **CartDtos**:
  - `CartItemDto.cs`: Represents an item in the customer's cart.
  - `CustomerCartDto.cs`: Represents the customer's cart containing multiple items.

- **MembershipDtos**:
  - `MembershipDto.cs`: Represents a membership, including the rooms associated with it.
  - `MembershipToCreateDto.cs`: Used for creating new memberships.

- **MenuItemDtos**:
  - `MenuItemDto.cs`: Represents a menu item available in the system.
  - `MenuItemToCreateDto.cs`: Used for creating new menu items.

- **OrderDtos**:
  - `OrderDto.cs`: Represents an order placed by a customer.
  - `OrderItemDto.cs`: Represents an item within an order.
  - `OrderToReturnDto.cs`: Represents the details of an order to be returned to the client.

- **PaymentDtos**:
  - `PaymentDto.cs`: Represents payment details for bookings or orders.
  - `PaymentStatusDto.cs`: Represents the status of a payment.
  - `UpdatePaymentIntentDto.cs`: Used for updating the payment intent ID after creating a payment session.

- **PhotoSessionDtos**:
  - `PhotoSessionDto.cs`: Represents details of a photo session.
  - `PhotoSessionToCreateDto.cs`: Used for creating new photo sessions.

- **RoomDtos**:
  - `RoomDto.cs`: Represents a room available for booking.
  - `RoomToCreateDto.cs`: Used for creating new rooms.
  

## API Documentation

For detailed API documentation, please refer to [API_Documentation.md](./API_Documentation.md).


### Configuration
Ensure `appsettings.json` is properly configured:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=Shaghaf.APIs;Trusted_Connection=True;TrustServerCertificate=True",
    "IdentityConnection": "Server=.;Database=Shaghaf.Identity;Trusted_Connection=True;TrustServerCertificate=True",
    "Redis": "localhost"
  },
  "ApiBaseUrl": "https://localhost:7095",
  "Stripe": {
    "SecretKey": "sk_test_yourSecretKeyHere",
    "PublishableKey": "pk_test_yourPublishableKeyHere"
  },
  "Jwt": {
    "Key": "yourJwtKeyHere",
    "Issuer": "SecureApi",
    "Audience": "SecureApiUser",
    "DurationInDays": 30
  },
  "Twilio": {
    "AccountSID": "yourTwilioAccountSID",
    "AuthToken": "yourTwilioAuthToken",
    "TwilioPhoneNumber": "+1234567821"
  }


