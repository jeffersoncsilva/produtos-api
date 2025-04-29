using MediatR;
using FE.Application.Contracts;
using FE.ViewModels;

namespace FE.Application.Features.Products.GetProductsCommand;

public class GetProductHandler(IHttpClientFactory factory, IAuthenticationTokenService tokenService) : BaseHandler(factory, tokenService), IRequestHandler<GetProductRequest, BaseResponse<GetProductResponse?>>
{

	public async Task<BaseResponse<GetProductResponse?>> Handle(GetProductRequest request, CancellationToken ct)
	{
		try
		{
			var uri = $"api/products?page={request.Page}&pageSize={request.Size}";
			var response = await SendRequestAsync(HttpMethod.Get, uri, null, ct);
			return await HandleResponseAsync<GetProductResponse>(response, ct);
		}
		catch(Exception ex)
		{
			Console.WriteLine($"Erro não previsto. MESSAGE: {ex.Message}");
			return new BaseResponse<GetProductResponse?>()
			{
				Dado = null,
				Status = EStatusResponse.ErroNaoIdentificado
			};
		}
	}
}
