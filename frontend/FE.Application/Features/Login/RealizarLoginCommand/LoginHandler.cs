using MediatR;
using FE.Application.Contracts;
using FE.ViewModels;

namespace FE.Application.Features.Login.RealizarLoginCommand;

public class LoginHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService authService) : BaseHandler(httpFactory, authService), IRequestHandler<LoginRequest, BaseResponse<LoginResponse?>>
{
	private readonly IAuthenticationTokenService _authService = authService;

	public async Task<BaseResponse<LoginResponse?>> Handle(LoginRequest request, CancellationToken ct)
	{
		try
		{

			var content = Serialize(request);
			var uri = "api/auth";
			var response = await SendRequestAsync(HttpMethod.Post, uri, content, ct);
			var resultado = await HandleResponseAsync<LoginResponse>(response, ct);
			if (resultado is { Status: EStatusResponse.Ok, Dado.Success: true })
			{
				await _authService.SetTokenAsync(resultado.Dado.AccessToken!, ct);
			}

			return resultado;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Erro não previsto. MESSAGE: {ex.Message}");
			return new BaseResponse<LoginResponse?>
			{
				Dado = null,
				Status = EStatusResponse.ErroNaoIdentificado
			};
		}
	}
}