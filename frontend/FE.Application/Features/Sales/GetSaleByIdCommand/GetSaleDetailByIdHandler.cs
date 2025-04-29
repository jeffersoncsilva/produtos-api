using MediatR;
using FE.Application.Contracts;
using FE.ViewModels;


namespace FE.Application.Features.Sales.GetSaleByIdCommand;

public class GetSaleDetailByIdHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService tokenService) : BaseHandler(httpFactory, tokenService), IRequestHandler<GetSaleDetailByIdRequest, BaseResponse<GetSaleDetailByIdResponse?>>
{
	public async Task<BaseResponse<GetSaleDetailByIdResponse?>> Handle(GetSaleDetailByIdRequest request, CancellationToken ct)
	{
		try
		{
			var uri = $"api/sales/{request.Id}";
			var result = await SendRequestAsync(HttpMethod.Get, uri, null, ct);
			return await HandleResponseAsync<GetSaleDetailByIdResponse>(result, ct);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Erro não previsto. MESSAGE: {ex.Message}");
			return new BaseResponse<GetSaleDetailByIdResponse?>()
			{
				Dado = null,
				Status = EStatusResponse.ErroNaoIdentificado
			};
		}
	}
}
