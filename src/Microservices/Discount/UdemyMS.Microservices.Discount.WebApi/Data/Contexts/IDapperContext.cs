namespace UdemyMS.Microservices.Discount.WebApi.Data.Contexts;

public interface IDapperContext
{
    Task<bool> ExecuteAsync(string query, object? param = null, CancellationToken cancellationToken = default);
    Task<T?> FirstOrDefaultAsync<T>(string query, object? param = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> QueryAsync<T>(string query, object? param = null, CancellationToken cancellationToken = default);
}