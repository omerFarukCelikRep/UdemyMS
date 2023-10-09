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
}