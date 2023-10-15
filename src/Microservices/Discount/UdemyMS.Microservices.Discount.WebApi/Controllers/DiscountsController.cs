using Microsoft.AspNetCore.Mvc;
using UdemyMS.Common.Web.Controllers;
using UdemyMS.Common.Web.Services;
using UdemyMS.Microservices.Discount.WebApi.Models.Requests;
using UdemyMS.Microservices.Discount.WebApi.Services;

namespace UdemyMS.Microservices.Discount.WebApi.Controllers;

public class DiscountsController : BaseController
{
    private readonly IDiscountService _discountService;
    private readonly IIdentityService _identityService;
    public DiscountsController(IDiscountService discountService, IIdentityService identityService)
    {
        _discountService = discountService;
        _identityService = identityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _discountService.GetAllAsync(cancellationToken);
        return GetResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _discountService.GetByIdAsync(id, cancellationToken);

        return GetResult(result);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var userId = _identityService.UserId;
        var result = await _discountService.GetByCodeAndUserIdAsync(code, int.Parse(userId!), cancellationToken);

        return GetResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(DiscountCreateRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _discountService.SaveAsync(request, cancellationToken);

        return GetResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(DiscountUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _discountService.UpdateAsync(request, cancellationToken);

        return GetResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _discountService.DeleteAsync(id, cancellationToken);

        return GetResult(result);
    }
}
