using System.ComponentModel.DataAnnotations.Schema;

namespace Shaghaf.Core.Entities.OrderEntities
{
    public class OrderItem : BaseEntity
    {
        public OrderItem() { }

        public OrderItem(int quantity, decimal price)
        {
            Quantity = quantity;
            Price = price;
        }

        public MenuItemOrdered MenuItem { get; set; } // Owned entity for MenuItem

        public int Quantity { get; set; } // Quantity of the item ordered (may change from item to another)
        public decimal Price { get; set; } // Price of the item ordered (may change from item to another)

        [ForeignKey("Order")] // Foreign key to the Orders table
        public int OrderId { get; set; } // Foreign Key

        [InverseProperty("OrderItems")] // Navigation property
        public Order Order { get; set; }

        // Convenience properties for accessing MenuItem details
        public string Name => MenuItem.MenuItemName; // Access MenuItem's name
        public string PictureUrl => MenuItem.PictureUrl; // Access MenuItem's picture URL
    }
}
