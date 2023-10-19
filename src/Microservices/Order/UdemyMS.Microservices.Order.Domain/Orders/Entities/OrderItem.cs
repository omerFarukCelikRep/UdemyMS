using UdemyMS.Common.Core.Entities;

namespace UdemyMS.Microservices.Order.Domain.Orders.Entities;
public class OrderItem : BaseEntity
{
    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string Thumbnail { get; private set; }
    public decimal Price { get; private set; }

    public OrderItem(string productId, string productName, string thumbnail, decimal price)
    {
        ProductId = productId;
        ProductName = productName;
        Thumbnail = thumbnail;
        Price = price;
    }

    public void Update(string productName, string thumbnail, decimal price)
    {
        ProductName = productName;
        Thumbnail = thumbnail;
        Price = price;
    }
}
