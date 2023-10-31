using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Microservices.Order.Application.Dtos;
using UdemyMS.Microservices.Order.Infrastructure.Contexts;

namespace UdemyMS.Microservices.Order.Application.Queries;
public class OrderGetByUserIdQueryHandler : IRequestHandler<OrderGetByUserIdQuery, Result<List<OrderDto>>>
{
    private readonly OrderDbContext _context;
    public OrderGetByUserIdQueryHandler(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<OrderDto>>> Handle(OrderGetByUserIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Orders
            .Include(x => x.OrderItems)
            .Where(x => x.UserId == request.UserId)
            .Select(x => new OrderDto(x.Id, x.CreatedDate, x.Address, x.UserId, x.OrderItems
                .Select(oi => new OrderItemDto(oi.ProductId, oi.ProductName, oi.Thumbnail, oi.Price
                ))
                .ToList()))
            .ToListAsync(cancellationToken);

        return Result<List<OrderDto>>.Success(result, (int)HttpStatusCode.OK);
    }
}
