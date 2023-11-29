using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Storage.BLL.Requests.Stock;
using Storage.DAL.Repositories.Interfaces;
using E = Storage.DAL.Entities;

namespace Storage.BLL.Validators.Stock;

public class CreateStocksBatchRequestValidator : AbstractValidator<CreateStocksBatchRequest>
{
    public CreateStocksBatchRequestValidator(IRepository<E.Product> productRepository)
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .MustAsync(async (id, token) => await productRepository.AnyAsync(p => p.Id == id, token))
            .WithMessage("Product with this id does not exist");
    }
}