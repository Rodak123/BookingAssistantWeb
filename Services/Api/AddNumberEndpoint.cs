using System.Text.Json;
using BookingAssistantWeb.Models;

namespace BookingAssistantWeb.Services.Api
{
    public class AddNumberRequest
    {
        public int phoneId { get; set; }
        public int configId { get; set; }
    }

    public class AddNumberResponse
    {
        public int phoneNumberId { get; set; }
    }

    public static class AddNumberEndpoint
    {
        public static async Task<ApiResponse<AddNumberResponse>> FetchAddNumberEndpoint(this ApiService apiService, AddNumberRequest request)
        {
            string requestJson = JsonSerializer.Serialize(request);
            JsApiResponse? jsResponse = await apiService.FetchApiPost("/user/add-number", requestJson);

            if (jsResponse == null)
                return ApiResponse<AddNumberResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<AddNumberResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<AddNumberResponse>();
                return data != null
                    ? ApiResponse<AddNumberResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<AddNumberResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<AddNumberResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}