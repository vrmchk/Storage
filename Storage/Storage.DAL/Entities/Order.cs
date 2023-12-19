using Storage.Common.Enums;
using Storage.DAL.Entities.Base;

namespace Storage.DAL.Entities;

public class Order : BaseEntity<Guid>
{
    public Order()
    {
        OrderSelections = new HashSet<OrderSelection>();
    }

    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Amount { get; set; }
    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
    public  ICollection<OrderSelection> OrderSelections { get; set; }
}