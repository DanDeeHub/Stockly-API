using System.Text.Json.Serialization;

namespace Stockly.Infrastructure.Api.Contracts.Dtos.Errors;

public class ErrorResponseDto
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object>? Values { get; set; }
}