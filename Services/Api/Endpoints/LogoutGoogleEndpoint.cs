using System.Text.Json;
using BookingAssistantWeb.Models;

namespace BookingAssistantWeb.Services.Api.Endpoints
{
    public class LogoutGoogleResponse
    {
        public bool success { get; set; }
    }

    public static class LogoutGoogleEndpoint
    {
        public static async Task<ApiResponse<LogoutGoogleResponse>> FetchLogoutGoogleEndpoint(this ApiService apiService)
        {
            JsApiResponse? jsResponse = await apiService.FetchApiGet("/logout-google");

            if (jsResponse == null)
                return ApiResponse<LogoutGoogleResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<LogoutGoogleResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<LogoutGoogleResponse>();
                return data != null
                    ? ApiResponse<LogoutGoogleResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<LogoutGoogleResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<LogoutGoogleResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}