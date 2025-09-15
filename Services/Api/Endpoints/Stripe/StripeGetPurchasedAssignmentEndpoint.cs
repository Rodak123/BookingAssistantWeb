using System.Text.Json;
using BookingAssistantWeb.Models;

namespace BookingAssistantWeb.Services.Api.Endpoints.Stripe
{
    public class StripeGetPurchasedAssignmentRequest
    {
        public string sessionId { get; set; } = string.Empty;
    }

    public class StripeGetPurchasedAssignmentResponse
    {
        public PhoneNumberAssignment? phoneNumberAssignment { get; set; }
    }

    public static class StripeGetPurchasedAssignmentEndpoint
    {
        public static async Task<ApiResponse<StripeGetPurchasedAssignmentResponse>> FetchStripeGetPurchasedAssignmentEndpoint(this ApiService apiService, StripeGetPurchasedAssignmentRequest request)
        {
            string requestJson = JsonSerializer.Serialize(request);
            JsApiResponse? jsResponse = await apiService.FetchApiPost("/stripe/get-purchased-assignment", requestJson);

            if (jsResponse == null)
                return ApiResponse<StripeGetPurchasedAssignmentResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<StripeGetPurchasedAssignmentResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<StripeGetPurchasedAssignmentResponse>();
                return data != null
                    ? ApiResponse<StripeGetPurchasedAssignmentResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<StripeGetPurchasedAssignmentResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<StripeGetPurchasedAssignmentResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}