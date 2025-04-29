using MediatR;
using FE.Application.Contracts;
using FE.ViewModels;


namespace FE.Application.Features.Sales.GetSalesCommand;

public class GetSalesHandler(IHttpClientFactory factory, IAuthenticationTokenService tokenService) : BaseHandler(factory, tokenService), IRequestHandler<GetSalesRequest, BaseResponse<GetSalesResponse?>>
{
	public async Task<BaseResponse<GetSalesResponse?>> Handle(GetSalesRequest request, CancellationToken ct)
	{
		try
		{
			var uri = $"api/sales?page={request.Page}&pageSize={request.Size}";
			var result = await SendRequestAsync(HttpMethod.Get, uri, null, ct);
			return await HandleResponseAsync<GetSalesResponse>(result, ct);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Erro não previsto. MESSAGE: {ex.Message}");
			return new BaseResponse<GetSalesResponse?>()
			{
				Dado = null,
				Status = EStatusResponse.ErroNaoIdentificado
			};
		}
	}
}
