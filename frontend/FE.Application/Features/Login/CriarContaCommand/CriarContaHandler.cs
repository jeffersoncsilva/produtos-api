using MediatR;
using FE.Application.Contracts;
using FE.ViewModels;

namespace FE.Application.Features.Login.CriarContaCommand;

public class CriarContaHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService tokenService) : BaseHandler(httpFactory, tokenService), IRequestHandler<NovaContaRequest, BaseResponse<NovaContaResponse?>>
{
	public async Task<BaseResponse<NovaContaResponse?>> Handle(NovaContaRequest? request, CancellationToken ct)
	{
		if (request is null)
		{
			return new BaseResponse<NovaContaResponse?>
			{
				Dado = null,
				Status = EStatusResponse.ErroNaoIdentificado
			};
		}

		try
		{
			var content = Serialize(request);
			var uri = "api/user";
			var result = await SendRequestAsync(HttpMethod.Post, uri, content, ct);
			return await HandleResponseAsync<NovaContaResponse>(result, ct);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Erro não previsto. MESSAGE: {ex.Message}");
			return new BaseResponse<NovaContaResponse?>
			{
				Dado = null,
				Status = EStatusResponse.ErroNaoIdentificado
			};
		}
	}
}