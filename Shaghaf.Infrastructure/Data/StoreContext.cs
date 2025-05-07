using Microsoft.EntityFrameworkCore;
using Shaghaf.Core.Entities;
using Shaghaf.Core.Entities.BirthdayEntity;
using Shaghaf.Core.Entities.BookingEntities;
using Shaghaf.Core.Entities.MembershipEntity;
using Shaghaf.Core.Entities.RoomEntities;
using Shaghaf.Core.Entities.OrderEntities;

namespace Shaghaf.Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure RoomPlan enum to be stored as string
            modelBuilder.Entity<Room>()
                .Property(r => r.Plan)
                .HasConversion(
                    v => v.ToString(), // Convert enum to string for storage
                    v => (RoomPlan)Enum.Parse(typeof(RoomPlan), v) // Convert string back to enum when reading
                );

            // Configure RoomType enum to be stored as string
            modelBuilder.Entity<Room>()
                .Property(r => r.Type)
                .HasConversion(
                    v => v.ToString(), // Convert enum to string for storage
                    v => (RoomType)Enum.Parse(typeof(RoomType), v) // Convert string back to enum when reading
                );

            // Many-to-Many Relationship between Membership and Room
            modelBuilder.Entity<Membership>()
                .HasMany(m => m.Rooms)
                .WithMany(r => r.Memberships)
                .UsingEntity<Dictionary<string, object>>(
                    "MembershipRoom",
                    j => j.HasOne<Room>().WithMany().HasForeignKey("RoomId"),
                    j => j.HasOne<Membership>().WithMany().HasForeignKey("MembershipId")
                );

            // Configure the BookingStatus enum to be stored as string
            modelBuilder.Entity<Booking>()
                .Property(b => b.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (BookingStatus)Enum.Parse(typeof(BookingStatus), v)
                );

            // Configure the relationship between Booking and Orders
            modelBuilder.Entity<Booking>()
                .HasMany(b => b.Orders)
                .WithOne(o => o.Booking)
                .HasForeignKey(o => o.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the relationship between Cake and Birthday
            modelBuilder.Entity<Cake>()
                .HasOne(c => c.Birthday)
                .WithMany(b => b.Cakes)
                .HasForeignKey(c => c.BirthdayId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the relationship between Decoration and Birthday
            modelBuilder.Entity<Decoration>()
                .HasOne(d => d.Birthday)
                .WithMany(b => b.Decorations)
                .HasForeignKey(d => d.BirthdayId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the relationship between PhotoSession and Room
            modelBuilder.Entity<PhotoSession>()
                .HasOne(p => p.Room)
                .WithMany(r => r.PhotoSessions)
                .HasForeignKey(p => p.RoomId);

            // Configure the relationship between Room and Birthday
            modelBuilder.Entity<Birthday>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Birthdays)
                .HasForeignKey(b => b.RoomId);

            // Configure OrderStatus enum to be stored as string
            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion(
                    v => v.ToString(), // Convert enum to string
                    v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v) // Convert string back to enum
                );

            // Configure the relationship between Order and OrderItem
            // Configure owned entity MenuItemOrdered
            modelBuilder.Entity<OrderItem>()
                .OwnsOne(oi => oi.MenuItem, mo =>
                {
                    mo.Property(m => m.MenuItemName).IsRequired();
                    mo.Property(m => m.PictureUrl).IsRequired();
                });

            // Configure the relationship between Order and OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure properties for decimal fields to avoid truncation warnings
            modelBuilder.Entity<Room>()
                .Property(r => r.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Room>()
                .Property(r => r.Rate)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Membership>()
                .Property(m => m.Price)
                .HasColumnType("decimal(18,2)");

            //modelBuilder.Entity<Advertisement>()
            //    .Property(a => a.Price)
            //    .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<MenuItem>()
                .Property(mi => mi.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");
        }

       // public DbSet<Home> Homes { get; set; }
        //public DbSet<Advertisement> Advertisements { get; set; }
        //public DbSet<Category> Categories { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Birthday> Birthdays { get; set; }
        public DbSet<Cake> Cakes { get; set; }
        public DbSet<Decoration> Decorations { get; set; }
        public DbSet<PhotoSession> PhotoSessions { get; set; }
       // public DbSet<Location> Locations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
