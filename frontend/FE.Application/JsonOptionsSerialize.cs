using System.Text.Json;
using System.Text.Json.Serialization;

namespace FE.Application;

public static class JsonOptionsSerialize
{
	public static JsonSerializerOptions JsonOptions { get; } = new JsonSerializerOptions()
	{
		ReferenceHandler = ReferenceHandler.Preserve
	};
}