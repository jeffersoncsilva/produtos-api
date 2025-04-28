using System.Text.Json;
using System.Text.Json.Serialization;

namespace RO.DevTest.FronEnd.Application;

public static class JsonOptionsSerialize
{
	public static JsonSerializerOptions JsonOptions { get; } = new JsonSerializerOptions()
	{
		ReferenceHandler = ReferenceHandler.Preserve
	};
}