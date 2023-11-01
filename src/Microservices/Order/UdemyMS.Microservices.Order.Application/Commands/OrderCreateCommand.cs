using MediatR;
using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Microservices.Order.Application.Dtos;

namespace UdemyMS.Microservices.Order.Application.Commands;
public class OrderCreateCommand : IRequest<Result<CreatedOrderDto>>
{
    public string UserId { get; set; }
    public AddressDto Address { get; set; }
    public List<OrderItemDto> Orders { get; set; }
}
