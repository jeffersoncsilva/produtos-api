using FE.ViewModels;
using MediatR;
using System.Text.Json.Serialization;

namespace FE.Application.Features.Products.UpdateProductCommand;

public class UpdateProductRequest : IRequest<BaseResponse<UpdateProductResponse?>>
{
	[JsonPropertyName("id")]
	public Guid Id { get; set; }
	[JsonPropertyName("Name")]
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
	[JsonPropertyName("modified_by")]
	public string? ModifiedBy { get; set; }

	[JsonPropertyName("created_by")]
	public string? CreatedBy { get; set; }
}
