namespace UdemyMS.Common.Core.ValueObjects;
public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        return Equals(left, right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !Equals(left, right);
    }


    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not ValueObject valueObject)
            return false;

        return GetEqualityComponents()
            .SequenceEqual(valueObject.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    public ValueObject? GetCopy()
    {
        return MemberwiseClone() as ValueObject;
    }
}