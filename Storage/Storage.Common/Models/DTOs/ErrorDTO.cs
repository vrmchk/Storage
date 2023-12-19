namespace Storage.Common.Models.DTOs;

public class ErrorDTO
{
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}