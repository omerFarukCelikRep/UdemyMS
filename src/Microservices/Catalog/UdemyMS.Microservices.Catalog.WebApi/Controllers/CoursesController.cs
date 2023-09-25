using Microsoft.AspNetCore.Mvc;
using UdemyMS.Common.Web.Controllers;
using UdemyMS.Microservices.Catalog.Interfaces.Services;

namespace UdemyMS.Microservices.Catalog.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CoursesController : BaseController
{
    private readonly ICourseService _courseService;
    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }
}