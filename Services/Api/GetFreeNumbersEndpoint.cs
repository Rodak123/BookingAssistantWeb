using System.Text.Json;
using BookingAssistantWeb.Models;

namespace BookingAssistantWeb.Services.Api
{
    public class GetFreeNumbersResponse
    {
        public List<PhoneNumber> phoneNumbers { get; set; } = new();
    }

    public static class GetFreeNumbersEndpoint
    {
        public static async Task<ApiResponse<GetFreeNumbersResponse>> FetchGetFreeNumbersEndpoint(this ApiService apiService)
        {
            JsApiResponse? jsResponse = await apiService.FetchApiGet("/numbers/free");

            if (jsResponse == null)
                return ApiResponse<GetFreeNumbersResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<GetFreeNumbersResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<GetFreeNumbersResponse>();
                return data != null
                    ? ApiResponse<GetFreeNumbersResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<GetFreeNumbersResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<GetFreeNumbersResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}