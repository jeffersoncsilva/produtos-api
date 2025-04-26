using RO.DevTest.FrontEnd.ViewModels.Features.Product;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace RO.DevTest.FronEnd.Application.Features.Products.GetProductByIdCommand;

public class GetProductByIdResponse
{
	[JsonPropertyName("id")]
	public Guid Id { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("description")]
	public string? Description { get; set; }

	[JsonPropertyName("price")]
	public decimal Price { get; set; }

	[JsonPropertyName("image_url")]
	public string? ImageUrl { get; set; }

	[JsonPropertyName("category")]
	public string? Category { get; set; }

	[JsonPropertyName("brand")]
	public string? Brand { get; set; }

	[JsonPropertyName("stock")]
	public int Stock { get; set; }

	[JsonPropertyName("is_active")]
	public bool IsActive { get; set; }

	[JsonPropertyName("created_by")]
	public string? CreatedBy { get; set; }

	[JsonPropertyName("modified_by")]
	public string? ModifiedBy { get; set; }

	[JsonPropertyName("created_on")]
	public DateTime CreatedOn { get; set; }

	[JsonPropertyName("modified_on")]
	public DateTime ModifiedOn { get; set; }

	
	public ProductCompleteViewModel ToViewModel()
	{
		return new ProductCompleteViewModel()
		{
			Id = Id,
			Name = Name,
			Description = Description,
			Price = Price,
			ImageUrl = ImageUrl,
			Category = Category,
			Brand = Brand,
			Stock = Stock,
			IsActive = IsActive,
			CreatedBy = CreatedBy,
			ModifiedBy = ModifiedBy,
			CreatedOn = CreatedOn,
			ModifiedOn = ModifiedOn
		};
	}
}
