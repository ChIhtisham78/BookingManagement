using Microsoft.AspNetCore.Http;

namespace Shaghaf.Core.Dtos.MenuItemDtos
{
    // DTO for creating a menu item
    public class MenuItemToCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public IFormFile Picture { get; set; }  // Picture field
    }
}
