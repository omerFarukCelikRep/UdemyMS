using Microsoft.AspNetCore.Mvc;
using UdemyMS.Common.Web.Controllers;
using UdemyMS.Common.Web.Services;
using UdemyMS.Microservices.Basket.WebApi.Models.Requests;
using UdemyMS.Microservices.Basket.WebApi.Services;

namespace UdemyMS.Microservices.Basket.WebApi.Controllers;
public class BasketsController : BaseController
{
    private readonly IBasketService _basketService;
    private readonly IIdentityService _identityService;
    public BasketsController(IBasketService basketService, IIdentityService identityService)
    {
        _basketService = basketService;
        _identityService = identityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken = default)
    {
        var result = await _basketService.GetAsync(_identityService.UserId, cancellationToken);

        return GetResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrUpdateAsync(BasketCreateRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _basketService.SaveOrUpdateAsync(request, cancellationToken);

        return GetResult(result);
    }
}