namespace Shaghaf.Core.Dtos.MenuItemDtos
{
    // DTO for transferring menu item data
    public class MenuItemDto
    {
        public int Id { get; set; }  // Id field for updating the item
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string PictureUrl { get; set; }  // URL of the picture for later access
    }
}
