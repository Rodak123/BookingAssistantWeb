using System.Text.Json;

namespace BookingAssistantWeb.Services.Api.Endpoints.Stripe
{
    public class StripeCreatePortalSessionRequest
    {
        public string sessionId { get; set; } = "";
        public string? returnUrl { get; set; }
    }

    public class StripeCreatePortalSessionResponse
    {
        public string? sessionUrl { get; set; }
    }

    public static class StripeCreatePortalSessionEndpoint
    {
        public static async Task<ApiResponse<StripeCreatePortalSessionResponse>> FetchStripeCreatePortalSessionEndpoint(this ApiService apiService, StripeCreatePortalSessionRequest request)
        {
            string requestJson = JsonSerializer.Serialize(request);
            JsApiResponse? jsResponse = await apiService.FetchApiPost("/stripe/create-portal-session", requestJson);

            if (jsResponse == null)
                return ApiResponse<StripeCreatePortalSessionResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<StripeCreatePortalSessionResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<StripeCreatePortalSessionResponse>();
                return data != null
                    ? ApiResponse<StripeCreatePortalSessionResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<StripeCreatePortalSessionResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<StripeCreatePortalSessionResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}