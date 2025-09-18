using System.Text.Json;

namespace BookingAssistantWeb.Services.Api.Endpoints.Google
{
    public class GetGoogleClientIdResponse
    {
        public string clientId { get; set; } = "";
    }

    public static class GetGoogleClientIdEndpoint
    {
        public static async Task<ApiResponse<GetGoogleClientIdResponse>> FetchGetGoogleClientIdEndpoint(this ApiService apiService)
        {
            JsApiResponse? jsResponse = await apiService.FetchApiGet("/google-client-id");

            if (jsResponse == null)
                return ApiResponse<GetGoogleClientIdResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<GetGoogleClientIdResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<GetGoogleClientIdResponse>();
                return data != null
                    ? ApiResponse<GetGoogleClientIdResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<GetGoogleClientIdResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<GetGoogleClientIdResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}