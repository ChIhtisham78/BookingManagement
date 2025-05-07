using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos.MenuItemDtos;
using Shaghaf.Core.Services.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.APIs.Controllers;

namespace Shaghaf.API.Controllers
{
    // Indicates that this is an API controller and sets the base route for all actions in this controller
    public class MenuItemsController : BaseApiController
    {
        private readonly IMenuItemService _menuItemService;

        // Constructor that accepts a service to handle menu item operations
        public MenuItemsController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        // Action to create a new menu item
        [HttpPost]
        public async Task<IActionResult> CreateMenuItem([FromForm] MenuItemToCreateDto menuItemToCreateDto)
        {
            var result = await _menuItemService.CreateMenuItemAsync(menuItemToCreateDto);
            return Ok(result);
        }

        // Action to update an existing menu item by its ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItem(int id, [FromForm] MenuItemToCreateDto menuItemToCreateDto)
        {
            await _menuItemService.UpdateMenuItemAsync(id, menuItemToCreateDto);
            return NoContent();
        }

        // Action to get a menu item by its ID
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDto>> GetMenuItemById(int id)
        {
            var result = await _menuItemService.GetMenuItemByIdAsync(id);
            if (result == null)
                return NotFound("Menu item not found.");
            return Ok(result);
        }

        // Action to get all menu items
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<MenuItemDto>>> GetAllMenuItems()
        {
            var result = await _menuItemService.GetAllMenuItemsAsync();
            return Ok(result);
        }

        // Action to get menu items by category
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IReadOnlyList<MenuItemDto>>> GetMenuItemsByCategory(string category)
        {
            var result = await _menuItemService.GetMenuItemsByCategoryAsync(category);
            return Ok(result);
        }

        // Action to delete a menu item by its ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            await _menuItemService.DeleteMenuItemAsync(id);
            return NoContent();
        }
    }
}
