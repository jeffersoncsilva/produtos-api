using MediatR;
using FE.Application.Contracts;
using FE.ViewModels;

namespace FE.Application.Features.Products.GetProductByIdCommand;

public class GetProductByIdHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService tokenService) : BaseHandler(httpFactory, tokenService), IRequestHandler<GetProductByIdCommandRequest, BaseResponse<GetProductByIdResponse?>>
{
	public async Task<BaseResponse<GetProductByIdResponse?>> Handle(GetProductByIdCommandRequest request, CancellationToken ct)
	{
		try
		{
			var uri = $"api/products/{request.Id}";
			var response = await SendRequestAsync(HttpMethod.Get, uri, null, ct);
			return await HandleResponseAsync<GetProductByIdResponse>(response, ct);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Erro não previsto. MESSAGE: {ex.Message}");
			return new BaseResponse<GetProductByIdResponse?>()
			{
				Dado = null,
				Status = EStatusResponse.ErroNaoIdentificado
			};
		}
	}
}
