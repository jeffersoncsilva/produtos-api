using MediatR;
using FE.Application.Contracts;
using FE.ViewModels;

namespace FE.Application.Features.Products.UpdateProductCommand;

public class UpdateProductHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService tokenService) : BaseHandler(httpFactory, tokenService), IRequestHandler<UpdateProductRequest, BaseResponse<UpdateProductResponse?>>
{
	public async Task<BaseResponse<UpdateProductResponse?>> Handle(UpdateProductRequest request, CancellationToken ct)
	{
		try
		{
			var content = Serialize(request);
			var uri = "api/products";
			var response = await SendRequestAsync(HttpMethod.Put, uri, content, ct);
			return await HandleResponseAsync<UpdateProductResponse>(response, ct);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Erro não previsto. MESSAGE: {ex.Message}");
			return new BaseResponse<UpdateProductResponse?>()
			{
				Dado = null,
				Status = EStatusResponse.ErroNaoIdentificado
			};
		}
	}
}
