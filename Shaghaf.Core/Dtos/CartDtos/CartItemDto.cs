using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Dtos.CartDtos
{
    public class CartItemDto
    {
        [Required]
        public int Id { get; set; } // ID of the menu item

        [Required]
        public string Name { get; set; } // Name of the item

        [Required]
        public string PictureUrl { get; set; } // URL of the item's picture

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero!!")]
        public decimal Price { get; set; } // Price of the item, must be greater than zero

        [Required]
        public string Category { get; set; } // Category of the item

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least one item!!")]
        public int Quantity { get; set; } // Quantity of the item, must be at least one
    }

}
