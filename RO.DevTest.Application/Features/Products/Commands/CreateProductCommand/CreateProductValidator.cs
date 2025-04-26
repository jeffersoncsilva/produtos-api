using FluentValidation;

namespace RO.DevTest.Application.Features.Products.Commands.CreateProductCommand;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nome do produto e obrigatório.")
            .MaximumLength(100)
            .WithMessage("Nome do produto não pode ter mais de 100 caracteres.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Descrição e obrigatório.")
            .MaximumLength(1024)
            .WithMessage("Descrição não pode ter mais que 1024 caracteres.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Preço deve ser maior que R$ 0,00.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .WithMessage("URL da imagem e obrigatório.")
            .MaximumLength(512)
            .WithMessage("URL da imagem não pode ter mais que 512 caracteres.");
        
        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Categoria não pode ser vaia.")
            .MaximumLength(64)
            .WithMessage("Categoria pode ter no máximo 64 caracteres.");

        RuleFor(x => x.Brand)
            .NotEmpty()
            .WithMessage("Marca não pode ser vazia.")
            .MaximumLength(128)
            .WithMessage("Marca não pode ser maior que 128 caracteres.");

        RuleFor(x => x.Stock)
            .GreaterThan(0)
            .WithMessage("O estoque deve ser maior que 0.");
        
        RuleFor(x => x.IsActive)
            .NotEqual(false)
            .WithMessage("Não pode ter um produto inativo.");

        RuleFor(x => x.CreatedBy)
            .NotEmpty()
            .WithMessage("Quem criou não pode ser vazio.");
    }
}