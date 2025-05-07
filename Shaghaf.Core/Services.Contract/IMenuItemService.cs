using Shaghaf.Core.Dtos.MenuItemDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shaghaf.Core.Services.Contract
{
    // Interface for menu item services
    public interface IMenuItemService
    {
        Task<MenuItemDto> CreateMenuItemAsync(MenuItemToCreateDto menuItemToCreateDto);
        Task UpdateMenuItemAsync(int id, MenuItemToCreateDto menuItemToCreateDto);
        Task<MenuItemDto?> GetMenuItemByIdAsync(int id);
        Task<IReadOnlyList<MenuItemDto>> GetAllMenuItemsAsync();
        Task<IReadOnlyList<MenuItemDto>> GetMenuItemsByCategoryAsync(string category);
        Task DeleteMenuItemAsync(int id);  // Method for deleting a menu item
    }
}
