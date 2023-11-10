using Storage.DAL.Entities.Base;

namespace Storage.DAL.Entities;

public class ProductReservation : BaseEntity<Guid>
{
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }

    public Guid ProductId { get; set; }

    public Product Product { get; set; } = null!;
}