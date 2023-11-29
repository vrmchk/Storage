namespace Storage.Common.Models.DTOs.Order;

public class SearchOrderDTO
{
    public List<Guid>? UserIds { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public List<string>? Statuses { get; set; }
    public List<Guid>? ProductIds { get; set; }
    public List<Guid>? OrderIds { get; set; }
}