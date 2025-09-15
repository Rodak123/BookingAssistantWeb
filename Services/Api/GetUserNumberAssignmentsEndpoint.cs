using System.Text.Json;
using BookingAssistantWeb.Models;

namespace BookingAssistantWeb.Services.Api
{
    public class GetUserNumberAssignmentsResponse
    {
        public List<PhoneNumberAssignment> phoneNumbersAssignments { get; set; } = new();
    }

    public static class GetUserNumberAssignmentsEndpoint
    {
        public static async Task<ApiResponse<GetUserNumberAssignmentsResponse>> FetchGetUserNumberAssignmentsEndpoint(this ApiService apiService)
        {
            JsApiResponse? jsResponse = await apiService.FetchApiGet("/user/numberAssignments");

            if (jsResponse == null)
                return ApiResponse<GetUserNumberAssignmentsResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<GetUserNumberAssignmentsResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<GetUserNumberAssignmentsResponse>();
                return data != null
                    ? ApiResponse<GetUserNumberAssignmentsResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<GetUserNumberAssignmentsResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<GetUserNumberAssignmentsResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}