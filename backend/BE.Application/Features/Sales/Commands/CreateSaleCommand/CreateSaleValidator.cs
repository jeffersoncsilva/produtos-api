using FluentValidation;

namespace BE.Application.Features.Sales.Commands.CreateSaleCommand;

public class CreateSaleValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty()
            .WithMessage("O nome de uma venda não pode ser vazio.")
            .MaximumLength(128)
            .WithMessage("O nome não pode ser maior que 128 caracteres.");

        RuleFor(s => s.Observation)
            .MaximumLength(2048)
            .WithMessage("Observação não pode ter mais de 2048 caracteres.");
        
        RuleFor(s => s.Descount)
            .GreaterThanOrEqualTo(0M)
            .WithMessage("Desconto não pode ser menor que R$ 0,00.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0M)
            .WithMessage("Preço não pode ser menor que R$ 0,00.");

        RuleFor(x => x.Products)
            .NotEmpty()
            .WithMessage("Produtos não pode ser vazio.");

        RuleForEach(x => x.Products).SetValidator(new ProductSaleValidator());

    }
}

public sealed class ProductSaleValidator : AbstractValidator<ProductSaleCommand>
{
	public ProductSaleValidator()
	{
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Id de produto não pode ser vázio.");
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantidade deve ser maior que 0.");
    }
}