namespace Storage.Common.Models.DTOs.User;

public class SearchUsersDTO
{
    public List<Guid>? UserIds { get; set; }
    public List<string>? Emails { get; set; }
    public List<string>? UserNames { get; set; }
}