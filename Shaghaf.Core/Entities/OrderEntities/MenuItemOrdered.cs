namespace Shaghaf.Core.Entities.OrderEntities
{
    public class MenuItemOrdered
    {
        public MenuItemOrdered() { }

        public MenuItemOrdered(int menuItemId, string menuItemName, string pictureUrl)
        {
            MenuItemId = menuItemId;
            MenuItemName = menuItemName;
            PictureUrl = pictureUrl;
        }

        public int MenuItemId { get; set; } // ID of the menu item that exist at DataBase
        public string MenuItemName { get; set; }
        public string PictureUrl { get; set; }
    }
}
