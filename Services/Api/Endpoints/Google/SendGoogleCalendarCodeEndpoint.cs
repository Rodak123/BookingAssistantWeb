using System.Text.Json;

namespace BookingAssistantWeb.Services.Api.Endpoints.Google
{
    public class GoogleCalendarCodeResponse
    {
        public bool success { get; set; }
    }


    public class GoogleCalendarRequest
    {
        public string code { get; set; } = "";
    }

    public static class GoogleCalendarCodeEndpoint
    {
        public static async Task<ApiResponse<GoogleCalendarCodeResponse>> FetchGoogleCalendarCodeEndpoint(this ApiService apiService, GoogleCalendarRequest request)
        {
            string requestJson = JsonSerializer.Serialize(request);
            JsApiResponse? jsResponse = await apiService.FetchApiPost("/oauth2/calendar", requestJson);

            if (jsResponse == null)
                return ApiResponse<GoogleCalendarCodeResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<GoogleCalendarCodeResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<GoogleCalendarCodeResponse>();
                return data != null
                    ? ApiResponse<GoogleCalendarCodeResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<GoogleCalendarCodeResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<GoogleCalendarCodeResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}