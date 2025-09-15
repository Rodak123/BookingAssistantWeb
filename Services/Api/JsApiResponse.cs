using System.Text.Json;

namespace BookingAssistantWeb.Services.Api
{
    public class JsApiResponse
    {
        public string Type { get; set; } = string.Empty;
        public int Status { get; set; }
        public string? Message { get; set; }
        public JsonElement? Data { get; set; }
    }
}
