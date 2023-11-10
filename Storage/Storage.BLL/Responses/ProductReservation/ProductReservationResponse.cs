namespace Storage.BLL.Responses.ProductReservation;

public class ProductReservationResponse
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid ProductId { get; set; }
}