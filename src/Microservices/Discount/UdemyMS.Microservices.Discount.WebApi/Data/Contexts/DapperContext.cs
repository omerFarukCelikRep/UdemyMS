using Dapper;
using Npgsql;
using System.Data;

namespace UdemyMS.Microservices.Discount.WebApi.Data.Contexts;

public class DapperContext : IDapperContext
{
    public const string ConnectionName = "Default";

    private readonly IDbConnection _connection;
    public DapperContext(DapperOptions options)
    {
        ArgumentException.ThrowIfNullOrEmpty(options.Connection);
        _connection = new NpgsqlConnection(options.Connection);
    }

    public Task<IEnumerable<T>> QueryAsync<T>(string query, object? param = null, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(query);

        if (cancellationToken.IsCancellationRequested)
            return Task.FromCanceled<IEnumerable<T>>(cancellationToken);

        return _connection.QueryAsync<T>(query, param);
    }

    public async Task<T?> FirstOrDefaultAsync<T>(string query, object? param = null, CancellationToken cancellationToken = default)
    {
        var values = await QueryAsync<T>(query, param, cancellationToken);

        return values.FirstOrDefault();
    }

    public async Task<bool> ExecuteAsync(string query, object? param = null, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(query);

        if (cancellationToken.IsCancellationRequested)
            await Task.FromCanceled<bool>(cancellationToken);

        var affectedRows = await _connection.ExecuteAsync(query, param);

        return affectedRows > 0;
    }
}