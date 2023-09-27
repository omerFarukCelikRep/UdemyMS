using Microsoft.AspNetCore.Mvc;
using UdemyMS.Common.Core.Utilities.Results;

namespace UdemyMS.Common.Web.Controllers;
public class BaseController : ControllerBase
{
    [NonAction]
    public IActionResult GetResult(Result result) => new ObjectResult(result);

    [NonAction]
    public IActionResult GetResult<T>(Result<T> result) => new ObjectResult(result) { StatusCode = result.StatusCode };
}
