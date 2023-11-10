using Storage.Common.Enums;
using Storage.DAL.Entities.Base;

namespace Storage.DAL.Entities;

public class Product : BaseEntity<Guid>
{
    public Product()
    {
        ProductReservations = new HashSet<ProductReservation>();
        Stocks = new HashSet<Stock>();
    }

    public ProductType Type { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }

    public ICollection<ProductReservation> ProductReservations { get; set; }
    public ICollection<Stock> Stocks { get; set; }
}