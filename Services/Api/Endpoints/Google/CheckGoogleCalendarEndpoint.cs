using System.Text.Json;

namespace BookingAssistantWeb.Services.Api.Endpoints.Google
{
    public class CheckGoogleCalendarResponse
    {
        public bool hasAccess { get; set; }
    }

    public static class CheckGoogleCalendarEndpoint
    {
        public static async Task<ApiResponse<CheckGoogleCalendarResponse>> FetchCheckGoogleCalendarEndpoint(this ApiService apiService)
        {
            JsApiResponse? jsResponse = await apiService.FetchApiGet("/oauth2/has-calendar-access");

            if (jsResponse == null)
                return ApiResponse<CheckGoogleCalendarResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<CheckGoogleCalendarResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<CheckGoogleCalendarResponse>();
                return data != null
                    ? ApiResponse<CheckGoogleCalendarResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<CheckGoogleCalendarResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<CheckGoogleCalendarResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}