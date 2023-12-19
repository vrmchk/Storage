namespace Storage.BLL.Requests.Order.Models;

public class CreateOrderSelectionModel
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}