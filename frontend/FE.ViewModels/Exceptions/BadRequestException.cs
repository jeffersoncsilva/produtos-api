using System.Net;

namespace FE.ViewModels.Exceptions;
public sealed class BadRequestException(HttpStatusCode statusCode, string message) : Exception(message)
{
	public HttpStatusCode StatusCode { get; init; } = statusCode;
}
