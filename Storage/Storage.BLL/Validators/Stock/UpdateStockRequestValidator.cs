using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Storage.BLL.Requests.Stock;
using Storage.DAL.Entities;
using Storage.DAL.Repositories.Interfaces;

namespace Storage.BLL.Validators.Stock;

public class UpdateStockRequestValidator : AbstractValidator<UpdateStockRequest>
{
    public UpdateStockRequestValidator(IRepository<OrderSelection> orderSelectionRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.OrderSelectionId)
            .NotEmpty()
            .MustAsync(async (id, token) => await orderSelectionRepository.AnyAsync(os => os.Id == id, token))
            .WithMessage("Order selection with this id does not exist");
    }
}