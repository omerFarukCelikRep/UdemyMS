using Dapper;
using Npgsql;
using System.Data;

namespace UdemyMS.Microservices.Discount.WebApi.Data.Contexts;

public class DapperContext
{
    public const string ConnectionName = "Default";

    private readonly IDbConnection _connection;
    public DapperContext(DapperOptions options)
    {
        ArgumentException.ThrowIfNullOrEmpty(options.Connection);
        _connection = new NpgsqlConnection(options.Connection);
    }

    public Task<IEnumerable<T>> QueryAsync<T>(string query, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(query);

        if (cancellationToken.IsCancellationRequested)
            return Task.FromCanceled<IEnumerable<T>>(cancellationToken);

        return _connection.QueryAsync<T>(query);
    }
}