using MediatR;
using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Microservices.Order.Application.Dtos;

namespace UdemyMS.Microservices.Order.Application.Queries;
public class OrderGetByUserIdQuery : IRequest<Result<List<OrderDto>>>
{
    public string UserId { get; set; }
}
