using MediatR;
using FE.Application.Contracts;
using FE.ViewModels;


namespace FE.Application.Features.Sales.CreateSaleCommand;

public class CreateSaleHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService tokenService) : BaseHandler(httpFactory, tokenService), IRequestHandler<CreateSaleRequest, BaseResponse<CreateSaleResponse?>>
{
	public async Task<BaseResponse<CreateSaleResponse?>> Handle(CreateSaleRequest request, CancellationToken ct)
	{
		try
		{

			var content = Serialize(request);
			var uri = "api/sales";
			var result = await SendRequestAsync(HttpMethod.Post, uri, content, ct);
			return await HandleResponseAsync<CreateSaleResponse>(result, ct);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Erro não previsto. MESSAGE: {ex.Message}");
			return new BaseResponse<CreateSaleResponse?>()
			{
				Dado = null,
				Status = EStatusResponse.ErroNaoIdentificado
			};

		}
	}
}
