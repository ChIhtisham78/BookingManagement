using Shaghaf.Core.Dtos.CartDtos;

public class CustomerCartDto
{
    public string Id { get; set; }
    public List<CartItemDto> Items { get; set; }
    public string PaymentIntentId { get; set; }
    public bool PaymentStatus { get; set; }
    public string SessionId { get; set; } 
}
