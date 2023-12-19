using Storage.DAL.Entities.Base;

namespace Storage.DAL.Entities;

public class Stock : BaseEntity<Guid>
{
    public Stock()
    {
        CreatedAt = DateTime.Now;
    }

    public DateTime CreatedAt { get; set; }

    public Guid ProductId { get; set; }
    public Guid? OrderSelectionId { get; set; }

    public Product Product { get; set; } = null!;
    public OrderSelection OrderSelection { get; set; } = null!;
}