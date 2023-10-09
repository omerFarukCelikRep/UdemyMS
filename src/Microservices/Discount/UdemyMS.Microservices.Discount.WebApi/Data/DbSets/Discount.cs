using UdemyMS.Common.Core.Entities;

namespace UdemyMS.Microservices.Discount.WebApi.Data.DbSets;

public class Discount : BaseEntity
{
    public string UserId { get; set; }
    public int Rate { get; set; }
    public string Code { get; set; }
}