using MediatR;
using FE.Application.Contracts;
using FE.ViewModels;

namespace FE.Application.Features.Sales.DeleteSaleCommand;

public sealed class DeleteSaleHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService tokenService) : BaseHandler(httpFactory, tokenService), IRequestHandler<DeleteSaleRequest, BaseResponse<DeleteSaleResponse?>>
{
	public async Task<BaseResponse<DeleteSaleResponse?>> Handle(DeleteSaleRequest request, CancellationToken ct)
	{
		try
		{
			var uri = $"api/sales/{request.Id}";
			var result = await SendRequestAsync(HttpMethod.Delete, uri, null, ct);
			return await HandleResponseAsync<DeleteSaleResponse>(result, ct);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Erro não previsto. MESSAGE: {ex.Message}");
			return new BaseResponse<DeleteSaleResponse?>()
			{
				Dado = null,
				Status = EStatusResponse.ErroNaoIdentificado
			};
		}
	}
}
