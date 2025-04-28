namespace FE.Application.Contracts;

public interface IAuthenticationTokenService
{
	Task<string?> GetTokenAsync(CancellationToken ct = default);
	ValueTask SetTokenAsync(string token, CancellationToken ct = default);

	ValueTask RemoveTokenAsync(CancellationToken ct = default);
}