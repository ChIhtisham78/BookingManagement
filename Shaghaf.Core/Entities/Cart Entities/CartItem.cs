namespace Shaghaf.Core.Entities.Cart_Entities
{
    // Class representing an item in the shopping cart
    public class CartItem
    {
        // ID of the item
        public int Id { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public int Quantity { get; set; }
    }
}
