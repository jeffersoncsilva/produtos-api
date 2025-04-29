using MediatR;
using FE.Application.Contracts;
using FE.ViewModels;


namespace FE.Application.Features.Sales.UpdateSaleCommand;

public class UpdateSaleHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService tokenService) : BaseHandler(httpFactory, tokenService), IRequestHandler<UpdateSaleRequest, BaseResponse<UpdateSaleResult?>>
{
	public async Task<BaseResponse<UpdateSaleResult?>> Handle(UpdateSaleRequest request, CancellationToken ct)
	{
		try
		{
			var content = Serialize(request);
			var uri = "api/sales";
			var result = await SendRequestAsync(HttpMethod.Put, uri, content, ct);
			return await HandleResponseAsync<UpdateSaleResult>(result, ct);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Erro não previsto. MESSAGE: {ex.Message}");
			return new BaseResponse<UpdateSaleResult?>()
			{
				Dado = null,
				Status = EStatusResponse.ErroNaoIdentificado
			};
		}
	}
}
