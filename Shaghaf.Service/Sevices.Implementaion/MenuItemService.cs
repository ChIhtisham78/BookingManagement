using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Shaghaf.Core;
using Shaghaf.Core.Dtos.MenuItemDtos;
using Shaghaf.Core.Entities;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Core.Specifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Shaghaf.Service.Sevices.Implementaion
{
    // Service for managing menu items
    public class MenuItemService : IMenuItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        // Constructor
        public MenuItemService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        // Method for creating a new menu item
        public async Task<MenuItemDto> CreateMenuItemAsync(MenuItemToCreateDto menuItemToCreateDto)
        {
            var menuItem = _mapper.Map<MenuItem>(menuItemToCreateDto);

            if (menuItemToCreateDto.Picture != null)
            {
                var pictureUrl = await SavePictureAsync(menuItemToCreateDto.Picture);
                menuItem.PictureUrl = pictureUrl;
            }

            _unitOfWork.Repository<MenuItem>().AddAsync(menuItem);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<MenuItemDto>(menuItem);
        }

        // Method for updating an existing menu item
        public async Task UpdateMenuItemAsync(int id, MenuItemToCreateDto menuItemToCreateDto)
        {
            var menuItem = await _unitOfWork.Repository<MenuItem>().GetByIdAsync(id);
            if (menuItem == null)
            {
                throw new KeyNotFoundException("No menu item found matching the specified criteria.");
            }

            _mapper.Map(menuItemToCreateDto, menuItem);

            if (menuItemToCreateDto.Picture != null)
            {
                var pictureUrl = await SavePictureAsync(menuItemToCreateDto.Picture);
                menuItem.PictureUrl = pictureUrl;
            }

            _unitOfWork.Repository<MenuItem>().Update(menuItem);
            await _unitOfWork.CompleteAsync();
        }

        // Method for getting a menu item by its ID
        public async Task<MenuItemDto?> GetMenuItemByIdAsync(int id)
        {
            var menuItem = await _unitOfWork.Repository<MenuItem>().GetByIdAsync(id);
            return menuItem == null ? null : _mapper.Map<MenuItemDto>(menuItem);
        }

        // Method for getting all menu items
        public async Task<IReadOnlyList<MenuItemDto>> GetAllMenuItemsAsync()
        {
            var menuItems = await _unitOfWork.Repository<MenuItem>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<MenuItemDto>>(menuItems);
        }

        // Method for getting menu items by category
        public async Task<IReadOnlyList<MenuItemDto>> GetMenuItemsByCategoryAsync(string category)
        {
            var spec = new MenuItemByCategorySpec(category);
            var menuItems = await _unitOfWork.Repository<MenuItem>().GetAllWithSpecAsync(spec);
            return _mapper.Map<IReadOnlyList<MenuItemDto>>(menuItems);
        }

        // Method for deleting a menu item by its ID
        public async Task DeleteMenuItemAsync(int id)
        {
            var menuItem = await _unitOfWork.Repository<MenuItem>().GetByIdAsync(id);
            if (menuItem == null)
            {
                throw new KeyNotFoundException("No menu item found matching the specified criteria.");
            }

            // Delete the picture if it exists
            if (!string.IsNullOrEmpty(menuItem.PictureUrl))
            {
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, menuItem.PictureUrl.Replace("/", "\\"));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            _unitOfWork.Repository<MenuItem>().Delete(menuItem);
            await _unitOfWork.CompleteAsync();
        }

        // Helper method to save the picture to the server
        private async Task<string> SavePictureAsync(IFormFile picture)
        {
            var uploadsFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "images", "menuitems");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + picture.FileName;
            var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await picture.CopyToAsync(fileStream);
            }

            return Path.Combine("images", "menuitems", uniqueFileName).Replace("\\", "/");
        }
    }
}
