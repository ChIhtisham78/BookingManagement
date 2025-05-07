using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Services.Contract;
using Talabat.APIs.Controllers;

namespace Shaghaf.API.Controllers
{
   
    public class HomeController : BaseApiController
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet("menu-items")]
        public async Task<IActionResult> GetMenuItems()
        {
            var menuItems = await _homeService.GetMenuItemsAsync();
            return Ok(menuItems);
        }

        [HttpGet("rooms")]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _homeService.GetRoomsAsync();
            return Ok(rooms);
        }

        [HttpGet("memberships")]
        public async Task<IActionResult> GetMemberships()
        {
            var memberships = await _homeService.GetMembershipsAsync();
            return Ok(memberships);
        }

        [HttpGet("birthdays")]
        public async Task<IActionResult> GetBirthdays()
        {
            var birthdays = await _homeService.GetBirthdaysAsync();
            return Ok(birthdays);
        }

        [HttpGet("photosessions")]
        public async Task<IActionResult> GetPhotoSessions()
        {
            var photoSessions = await _homeService.GetPhotoSessionsAsync();
            return Ok(photoSessions);
        }
    }
}
