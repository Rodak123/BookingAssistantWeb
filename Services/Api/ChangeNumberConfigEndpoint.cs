using System.Text.Json;

namespace BookingAssistantWeb.Services.Api
{
    public class ChangeNumberConfigRequest
    {
        public int configId { get; set; }

        public int bookingWindowSeconds { get; set; }
        public int bookingMarginSeconds { get; set; }
        public int bookingIntervalSeconds { get; set; }
    }

    public class ChangeNumberConfigResponse
    {
        public int configId { get; set; }
    }

    public static class ChangeNumberConfigEndpoint
    {
        public static async Task<ApiResponse<ChangeNumberConfigResponse>> FetchAddNumberConfigEndpoint(this ApiService apiService, ChangeNumberConfigRequest request)
        {
            string requestJson = JsonSerializer.Serialize(request);
            JsApiResponse? jsResponse = await apiService.FetchApiPost("/config/change", requestJson);

            if (jsResponse == null)
                return ApiResponse<ChangeNumberConfigResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<ChangeNumberConfigResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<ChangeNumberConfigResponse>();
                return data != null
                    ? ApiResponse<ChangeNumberConfigResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<ChangeNumberConfigResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<ChangeNumberConfigResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}