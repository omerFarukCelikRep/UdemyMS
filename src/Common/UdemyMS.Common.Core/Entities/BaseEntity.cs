namespace UdemyMS.Common.Core.Entities;

public class BaseEntity : BaseEntity<int>
{

}

public class BaseEntity<TId>
{
    public TId Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
