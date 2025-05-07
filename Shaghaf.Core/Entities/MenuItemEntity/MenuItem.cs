namespace Shaghaf.Core.Entities
{
    // Entity representing a menu item
    public class MenuItem : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } // e.g., "Hot Drinks", "Cold Drinks", "Snacks"
        public string PictureUrl { get; set; }
    }
}
