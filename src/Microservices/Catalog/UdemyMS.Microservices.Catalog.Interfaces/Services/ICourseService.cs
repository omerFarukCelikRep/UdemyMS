using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Microservices.Catalog.Entities.Dtos.Courses;

namespace UdemyMS.Microservices.Catalog.Interfaces.Services;
public interface ICourseService
{
    Task<Result<CourseDetailsDto>> CreateAsync(CourseCreateDto courseCreate, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(string courseId, CancellationToken cancellationToken = default);
    Task<Result<List<CourseListDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<List<CourseListDto>>> GetAllByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result<CourseDetailsDto>> GetByIdAsync(string courseId, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(CourseUpdateDto courseUpdate, CancellationToken cancellationToken = default);
}
