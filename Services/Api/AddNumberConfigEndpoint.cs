using System.Text.Json;

namespace BookingAssistantWeb.Services.Api
{
    public class AddNumberConfigRequest
    {
        public int bookingWindowSeconds { get; set; }
        public int bookingMarginSeconds { get; set; }
        public int bookingIntervalSeconds { get; set; }
    }

    public class AddNumberConfigResponse
    {
        public int configId { get; set; }
    }

    public static class AddNumberConfigEndpoint
    {
        public static async Task<ApiResponse<AddNumberConfigResponse>> FetchAddNumberConfigEndpoint(this ApiService apiService, AddNumberConfigRequest request)
        {
            string requestJson = JsonSerializer.Serialize(request);
            JsApiResponse? jsResponse = await apiService.FetchApiPost("/user/add-config", requestJson);

            if (jsResponse == null)
                return ApiResponse<AddNumberConfigResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<AddNumberConfigResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<AddNumberConfigResponse>();
                return data != null
                    ? ApiResponse<AddNumberConfigResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<AddNumberConfigResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<AddNumberConfigResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}