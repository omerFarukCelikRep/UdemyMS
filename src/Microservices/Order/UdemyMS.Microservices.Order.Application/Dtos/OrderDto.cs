namespace UdemyMS.Microservices.Order.Application.Dtos;
public record OrderDto(int Id,
                       DateTime CreatedDate,
                       AddressDto Address,
                       string UserId,
                       List<OrderItemDto> OrderItems)
{
    public static implicit operator OrderDto(Domain.Orders.Order order) => new(
        order.Id,
        order.CreatedDate,
        order.Address,
        order.UserId,
        order.OrderItems.Select(x => (OrderItemDto)x)
                        .ToList());
}