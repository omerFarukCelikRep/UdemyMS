using MediatR;
using System.Net;
using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Microservices.Order.Application.Dtos;
using UdemyMS.Microservices.Order.Infrastructure.Contexts;

namespace UdemyMS.Microservices.Order.Application.Commands;

public class OrderCreateCommandHandler : IRequestHandler<OrderCreateCommand, Result<CreatedOrderDto>>
{
    private readonly OrderDbContext _context;
    public OrderCreateCommandHandler(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CreatedOrderDto>> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
    {
        Domain.Orders.Order order = new(request.UserId, request.Address);
        request.Orders.ForEach(x => order.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.Thumbnail));

        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<CreatedOrderDto>.Success(new CreatedOrderDto { Id = order.Id }, (int)HttpStatusCode.OK);
    }
}