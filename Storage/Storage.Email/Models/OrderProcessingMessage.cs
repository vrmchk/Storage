using Storage.Email.Models.Base;

namespace Storage.Email.Models;

public class OrderProcessingMessage : EmailMessageBase
{
    public override string Subject => "Storage order";
    public override string TemplateName => nameof(OrderProcessingMessage);

    public string UserName { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
}