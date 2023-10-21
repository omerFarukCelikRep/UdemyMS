namespace UdemyMS.Common.Core.Entities;

public abstract class BaseEntity : BaseEntity<int>
{

}

public abstract class BaseEntity<TId> where TId : struct
{
    public TId Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public bool IsTransient() => Id.Equals((TId)default);

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not BaseEntity<TId> entity)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        if (entity.IsTransient() || IsTransient())
            return false;

        return entity.Id.Equals(Id);
    }

    public static bool operator ==(BaseEntity<TId> left, BaseEntity<TId> right)
    {
        if (Equals(left, null))
            return Equals(right, null);

        return left.Equals(right);
    }

    public static bool operator !=(BaseEntity<TId> left, BaseEntity<TId> right)
    {
        if (Equals(left, null))
            return !Equals(right, null);

        return !left.Equals(right);
    }
}