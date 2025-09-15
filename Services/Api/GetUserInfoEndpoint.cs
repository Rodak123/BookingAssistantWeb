using System.Text.Json;

namespace BookingAssistantWeb.Services.Api
{
    public class GetUserInfoResponse
    {
        public int? id { get; set; }
        public string? givenName { get; set; }
        public string? familyName { get; set; }
        public string? picture { get; set; }
        public string? locale { get; set; }
        public string? email { get; set; }
    }

    public static class GetUserInfoEndpoint
    {
        public static async Task<ApiResponse<GetUserInfoResponse>> FetchGetUserInfoEndpoint(this ApiService apiService)
        {
            JsApiResponse? jsResponse = await apiService.FetchApiGet("/user/info");

            if (jsResponse == null)
                return ApiResponse<GetUserInfoResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<GetUserInfoResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<GetUserInfoResponse>();
                return data != null
                    ? ApiResponse<GetUserInfoResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<GetUserInfoResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<GetUserInfoResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}