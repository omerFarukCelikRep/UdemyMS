using UdemyMS.Common.Core.Aggregates;
using UdemyMS.Common.Core.Entities;
using UdemyMS.Microservices.Order.Domain.Orders.Entities;
using UdemyMS.Microservices.Order.Domain.ValueObjects;

namespace UdemyMS.Microservices.Order.Domain.Orders;
public class Order : BaseEntity, IAggregateRoot
{
    private readonly List<OrderItem> _orderItems;

    public Address Address { get; private set; }

    public string UserId { get; private set; }

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public Order(string userId, Address address)
    {
        _orderItems = new List<OrderItem>();
        UserId = userId;
        Address = address;
    }

    public void AddOrderItem(string productId, string productName, decimal price, string thumbnail)
    {
        var exist = _orderItems.Exists(x => x.ProductId == productId);
        if (!exist)
        {
            var orderItem = new OrderItem(productId, productName, thumbnail, price);
            _orderItems.Add(orderItem);
        }
    }

    public decimal GetTotalPrice() => _orderItems.Sum(x => x.Price);
}