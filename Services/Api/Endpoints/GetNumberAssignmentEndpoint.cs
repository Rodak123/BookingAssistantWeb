using System.Text.Json;
using BookingAssistantWeb.Models;

namespace BookingAssistantWeb.Services.Api.Endpoints
{
    public class GetNumberAssignmentRequest
    {
        public int phoneNumberAssignmentId { get; set; }
    }

    public class GetNumberAssignmentResponse
    {
        public PhoneNumberAssignment? phoneNumberAssignment { get; set; }
    }

    public static class GetNumberAssignmentEndpoint
    {
        public static async Task<ApiResponse<GetNumberAssignmentResponse>> FetchGetNumberAssignmentEndpoint(this ApiService apiService, GetNumberAssignmentRequest request)
        {
            string requestJson = JsonSerializer.Serialize(request);
            JsApiResponse? jsResponse = await apiService.FetchApiPost("/numberAssignment", requestJson);

            if (jsResponse == null)
                return ApiResponse<GetNumberAssignmentResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<GetNumberAssignmentResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<GetNumberAssignmentResponse>();
                return data != null
                    ? ApiResponse<GetNumberAssignmentResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<GetNumberAssignmentResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<GetNumberAssignmentResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}