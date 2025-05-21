Booking Management Overview
Booking Management is a scalable .NET 7.0 application for booking and managing rooms for training, meetings, events, birthdays, and photo sessions. It also supports memberships, orders, and user account management.

Key Features
Room booking with real-time availability

Custom birthday packages and photo sessions

Memberships with exclusive access and discounts

Cart, ordering, and payment via Stripe

SMS verification using Twilio

Tech Stack
.NET 7, EF Core, MS SQL, Redis

JWT, ASP.NET Identity, AutoMapper

Follows Onion Architecture with layered structure

RESTful APIs using clean, maintainable code

Architecture
API: Handles requests and middleware

Core: Domain models & business logic

Infrastructure: Data access with Unit of Work & Generic Repository

Services: Business operations

Supports high performance with Redis caching, secure transactions, and modular design.
