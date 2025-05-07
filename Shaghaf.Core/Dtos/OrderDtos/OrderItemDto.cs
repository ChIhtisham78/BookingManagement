    using System.ComponentModel.DataAnnotations;

    namespace Shaghaf.Core.Dtos.OrderDtos
    {
        // DTO for individual order items
        public class OrderItemDto
        {
            public int Id { get; set; }
            public int MenuItemId { get; set; }
            public string MenuItemName { get; set; }
            public string PictureUrl { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
    }