using System.Text.Json;
using BookingAssistantWeb.Models;

namespace BookingAssistantWeb.Services.Api
{
    public class GetUserNumbersResponse
    {
        public List<PhoneNumber> phoneNumbers { get; set; } = new();
    }

    public static class GetUserNumbersEndpoint
    {
        public static async Task<ApiResponse<GetUserNumbersResponse>> FetchGetUserNumbersEndpoint(this ApiService apiService)
        {
            JsApiResponse? jsResponse = await apiService.FetchApiGet("/user/numbers");

            if (jsResponse == null)
                return ApiResponse<GetUserNumbersResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<GetUserNumbersResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<GetUserNumbersResponse>();
                return data != null
                    ? ApiResponse<GetUserNumbersResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<GetUserNumbersResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<GetUserNumbersResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}