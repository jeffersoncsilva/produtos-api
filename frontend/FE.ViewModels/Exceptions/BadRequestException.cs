
using System.Net;

namespace FE.ViewModels.Exceptions;

public sealed class BadRequestException : Exception
{
	public BadRequestException(HttpStatusCode code, string message) : base(message)
	{
		
	}

	public BadRequestException(string message) : base(message)
	{
		
	}
}
