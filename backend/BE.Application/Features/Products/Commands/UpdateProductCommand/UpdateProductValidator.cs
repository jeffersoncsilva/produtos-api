using FluentValidation;

namespace BE.Application.Features.Products.Commands.UpdateProductCommand;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommandRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(p => p.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Id não pode ser inválido.");
        
        RuleFor(p => p.ModifiedBy)
            .NotEmpty()
            .WithMessage("Quem atualizou não pode ser vazio.");
    }
}