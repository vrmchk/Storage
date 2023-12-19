namespace Storage.Common.Models.DTOs.Product;

public class UpdateProductDTO
{
    public string Type { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}