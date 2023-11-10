using Storage.DAL.Entities.Base;

namespace Storage.DAL.Entities;

public class OrderSelection : BaseEntity<Guid>
{
    public OrderSelection()
    {
        Stocks = new HashSet<Stock>();
    }

    public int Quantity { get; set; }

    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }

    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public ICollection<Stock> Stocks { get; set; }
}