namespace Shaghaf.Core.Entities.Cart_Entities
{

    public class CustomerCart
    {
        public string Id { get; set; } // Cart ID
        public List<CartItem> Items { get; set; } // List of items in the cart
        public string SessionId { get; set; } // Session ID for Stripe session
        public string PaymentIntentId { get; set; } // Payment Intent ID for Stripe integration
        public bool PaymentStatus { get; set; } // Payment status for polling

        public CustomerCart(string id)
        {
            Id = id;
            Items = new List<CartItem>(); // Initializes the list of items
        }
    }
}
