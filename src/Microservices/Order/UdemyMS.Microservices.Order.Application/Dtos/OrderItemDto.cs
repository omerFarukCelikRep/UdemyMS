using UdemyMS.Microservices.Order.Domain.Orders.Entities;

namespace UdemyMS.Microservices.Order.Application.Dtos;
public record OrderItemDto(string ProductId, string ProductName, string Thumbnail, decimal Price)
{
    public static implicit operator OrderItemDto(OrderItem orderItem) => new(
        orderItem.ProductId,
        orderItem.ProductName,
        orderItem.Thumbnail,
        orderItem.Price);
}