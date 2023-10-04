using UdemyMS.Common.Web.Controllers;
using UdemyMS.Common.Web.Services;
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
}