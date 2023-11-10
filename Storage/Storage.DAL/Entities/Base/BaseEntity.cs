namespace Storage.DAL.Entities.Base;

public abstract class BaseEntity<T>
    where T : IEquatable<T>
{
    public T Id { get; set; } = default!;
}