using System.Text.Json;
using BookingAssistantWeb.Models;

namespace BookingAssistantWeb.Services.Api
{
    public class GetNumberConfigRequest
    {
        public int configId { get; set; }
    }

    public class GetNumberConfigResponse
    {
        public PhoneNumberConfig? phoneNumberConfig { get; set; }
    }

    public static class GetNumberConfigEndpoint
    {
        public static async Task<ApiResponse<GetNumberConfigResponse>> FetchAddNumberConfigEndpoint(this ApiService apiService, GetNumberConfigRequest request)
        {
            string requestJson = JsonSerializer.Serialize(request);
            JsApiResponse? jsResponse = await apiService.FetchApiPost("/config", requestJson);

            if (jsResponse == null)
                return ApiResponse<GetNumberConfigResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<GetNumberConfigResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<GetNumberConfigResponse>();
                return data != null
                    ? ApiResponse<GetNumberConfigResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<GetNumberConfigResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<GetNumberConfigResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}