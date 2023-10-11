using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Microservices.Discount.WebApi.Data.Contexts;
using UdemyMS.Microservices.Discount.WebApi.Models.Dtos;

namespace UdemyMS.Microservices.Discount.WebApi.Services;

public class DiscountService : IDiscountService
{
    private readonly IDapperContext _context;
    public DiscountService(IDapperContext context)
    {
        _context = context;
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _context.ExecuteAsync(
            query: "Delete From Discount Where id=@Id",
            param: new { Id = id },
            cancellationToken: cancellationToken);
        if (!result)
            return Result.Error("Discount Not Found", StatusCodes.Status404NotFound);

        return Result.Success(StatusCodes.Status204NoContent);
    }

    public async Task<Result<List<DiscountListDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var discounts = await _context.QueryAsync<DiscountListDto>("Select * From Discount", cancellationToken: cancellationToken);

        return Result<List<DiscountListDto>>.Success(discounts.ToList(), StatusCodes.Status200OK);
    }

    public async Task<Result<DiscountDetailsDto>> GetByCodeAndUserIdAsync(string code, int userId, CancellationToken cancellationToken = default)
    {
        var discount = await _context.FirstOrDefaultAsync<DiscountDetailsDto>(
            query: "Select * From Discount Where userid=@UserId and code=@Code",
            param: new { UserId = userId, Code = code },
            cancellationToken: cancellationToken);
        if (discount is null)
            return Result<DiscountDetailsDto>.Error("Discount Not Found", StatusCodes.Status404NotFound); //TODO:Magic string

        return Result<DiscountDetailsDto>.Success(discount, StatusCodes.Status200OK);
    }

    public async Task<Result<DiscountDetailsDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var discount = await _context.FirstOrDefaultAsync<DiscountDetailsDto>(
            query: "Select * From Discount Where Id=@Id",
            param: new { id },
            cancellationToken: cancellationToken);
        if (discount is null)
            return Result<DiscountDetailsDto>.Error("Discount Not Found", StatusCodes.Status404NotFound); //TODO:Magic string

        return Result<DiscountDetailsDto>.Success(discount, StatusCodes.Status200OK);
    }

    public async Task<Result> SaveAsync(DiscountCreateDto discount, CancellationToken cancellationToken = default)
    {
        var result = await _context.ExecuteAsync(
            query: "Insert into Discount (userid,rate,code) Values(@UserId,@Rate,@Code)",
            param: discount,
            cancellationToken: cancellationToken);
        if (!result)
            return Result.Error("An error accured while adding", StatusCodes.Status500InternalServerError); //TODO:Magic string

        return Result.Success(StatusCodes.Status204NoContent);
    }

    public async Task<Result> UpdateAsync(DiscountUpdateDto discountUpdate, CancellationToken cancellationToken = default)
    {
        var result = await _context.ExecuteAsync(
            query: "Update Discount Set userid=@UserId, code=@Code, rate=@Rate Where id=@Id",
            param: new { discountUpdate.Id, discountUpdate.UserId, discountUpdate.Code, discountUpdate.Rate },
            cancellationToken: cancellationToken);
        if (!result)
            return Result.Error("An error accured while adding", StatusCodes.Status500InternalServerError); //TODO:Magic string

        return Result.Success(StatusCodes.Status204NoContent);
    }
}