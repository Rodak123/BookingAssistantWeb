using System.Text.Json;
using BookingAssistantWeb.Models;

namespace BookingAssistantWeb.Services.Api.Endpoints
{
    public class GetNumberRequest
    {
        public int phoneNumberId { get; set; }
    }

    public class GetNumberResponse
    {
        public PhoneNumber? phoneNumber { get; set; }
    }

    public static class GetNumberEndpoint
    {
        public static async Task<ApiResponse<GetNumberResponse>> FetchGetNumberEndpoint(this ApiService apiService, GetNumberRequest request)
        {
            string requestJson = JsonSerializer.Serialize(request);
            JsApiResponse? jsResponse = await apiService.FetchApiPost("/number", requestJson);

            if (jsResponse == null)
                return ApiResponse<GetNumberResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<GetNumberResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<GetNumberResponse>();
                return data != null
                    ? ApiResponse<GetNumberResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<GetNumberResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<GetNumberResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}