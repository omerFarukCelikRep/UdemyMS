using System.Net;
using System.Text.Json.Serialization;

namespace UdemyMS.Common.Core.Utilities.Results;

public record Result
{
    [JsonIgnore]
    public int StatusCode { get; init; }

    [JsonIgnore]
    public bool IsSuccess { get; init; }

    public IReadOnlyList<string> Errors { get; init; } = Enumerable.Empty<string>().ToList();

    public static Result NoContent() => new()
    {
        StatusCode = (int)HttpStatusCode.NoContent
    };

    public static Result Success(int statusCode) => new()
    {
        StatusCode = statusCode,
        IsSuccess = true
    };

    public static Result Error(List<string> errors, int statusCode) => new()
    {
        StatusCode = statusCode,
        Errors = errors.AsReadOnly(),
        IsSuccess = false
    };

    public static Result Error(string error, int statusCode) => new()
    {
        StatusCode = statusCode,
        Errors = new List<string>(1) { error }.AsReadOnly(),
        IsSuccess = false
    };
}

public record Result<T> : Result
{
    public T? Data { get; init; }

    public static Result<T> Success(T data, int statusCode) => new()
    {
        Data = data,
        StatusCode = statusCode,
        IsSuccess = true
    };

    public static new Result<T> Error(string error, int statusCode) => new()
    {
        StatusCode = statusCode,
        Errors = new List<string>(1) { error }.AsReadOnly(),
        IsSuccess = false
    };
}