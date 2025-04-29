using MediatR;
using FE.Application.Contracts;
using FE.ViewModels;

namespace FE.Application.Features.Products.CreateProductCommand;

public sealed class CreateProductHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService tokenService) : BaseHandler(httpFactory, tokenService),  IRequestHandler<CreateProductRequest, BaseResponse<CreateProductResponse?>>
{
	public async Task<BaseResponse<CreateProductResponse?>> Handle(CreateProductRequest request, CancellationToken ct)
	{
		try
		{
			var content = Serialize(request);
			var uri = "api/products";
			var result = await SendRequestAsync(HttpMethod.Post, uri, content, ct);
			return await HandleResponseAsync<CreateProductResponse>(result, ct);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Erro não previsto. MESSAGE: {ex.Message}");
			return new BaseResponse<CreateProductResponse?>()
			{
				Dado = null,
				Status = EStatusResponse.ErroNaoIdentificado
			};
		}
	}
}
