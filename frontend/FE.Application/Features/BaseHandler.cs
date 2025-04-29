using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using FE.Application.Contracts;
using FE.Application.Features.Products.GetProductsCommand;
using FE.ViewModels;
using FE.ViewModels.Exceptions;

namespace FE.Application.Features;

public abstract class BaseHandler(IHttpClientFactory factory, IAuthenticationTokenService tokenService)
{
	private static HttpClient? _httpClient;

	private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions()
	{
		ReferenceHandler = ReferenceHandler.Preserve
	};


	private async Task<HttpClient> HttpClient()
	{

		_httpClient ??= factory.CreateClient(HttpConfiguration.HttpClientName);
		var token = await tokenService.GetTokenAsync();
		if (!string.IsNullOrWhiteSpace(token))
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		return _httpClient;
	}

	protected async Task<T?> DeserializeAcync<T>(Stream stream, CancellationToken ct = default)
	{
		return await JsonSerializer.DeserializeAsync<T>(stream, _jsonOptions, ct);
	}

	protected StringContent Serialize<T>(T obj)
	{
		var jsonContent = new StringContent(JsonSerializer.Serialize(obj, _jsonOptions), Encoding.UTF8, "application/json");
		return jsonContent;
	}

	protected async Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string requestUri, HttpContent? content = null, CancellationToken ct = default)
	{
		using var request = new HttpRequestMessage(method, requestUri);
		if (content is not null)
		{
			request.Content = content;
		}

		var response = await (await HttpClient()).SendAsync(request, ct);
		return response;
	}

	protected async Task<BaseResponse<T?>> HandleResponseAsync<T>(HttpResponseMessage response, CancellationToken ct = default)
	{
		if (response.IsSuccessStatusCode)
		{
			if (response.Content.Headers.ContentLength == 0)
			{
				return default;
			}
			var stream = await response.Content.ReadAsStreamAsync(ct);
			var dado = await DeserializeAcync<T>(stream, ct);
			return new BaseResponse<T?>
			{
				Dado = dado,
				Status = EStatusResponse.Ok
			};
		}

		switch (response.StatusCode)
		{
			case HttpStatusCode.Unauthorized:
				{
					return new BaseResponse<T?>
					{
						Dado = default,
						Status = EStatusResponse.Unauthorized
					};
				}
			case HttpStatusCode.BadRequest:
				{
					return new BaseResponse<T?>
					{
						Dado = default,
						Status = EStatusResponse.BadRequest
					};
				}
			case HttpStatusCode.NotFound:
				{
					return new BaseResponse<T?>
					{
						Dado = default,
						Status = EStatusResponse.NotFound
					};
				}
			case HttpStatusCode.InternalServerError:
				{
					return new BaseResponse<T?>
					{
						Dado = default,
						Status = EStatusResponse.InternalServerError
					};
				}
			default:
				{
					return new BaseResponse<T?>
					{
						Dado = default,
						Status = EStatusResponse.ErroNaoIdentificado
					};
				}
		}
	}
}