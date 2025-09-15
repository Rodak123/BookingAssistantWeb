using System.Text.Json;

namespace BookingAssistantWeb.Services.Api
{
    public class StripeCreateChekoutSessionRequest
    {
        public string lookupKey { get; set; } = "basic_plan";
    }

    public class StripeCreateChekoutSessionResponse
    {
        public string? sessionUrl { get; set; }
    }

    public static class StripeCreateChekoutSessionEndpoint
    {
        public static async Task<ApiResponse<StripeCreateChekoutSessionResponse>> FetchStripeCreateChekoutSessionEndpoint(this ApiService apiService, StripeCreateChekoutSessionRequest request)
        {
            string requestJson = JsonSerializer.Serialize(request);
            JsApiResponse? jsResponse = await apiService.FetchApiPost("/stripe/create-checkout-session", requestJson);

            if (jsResponse == null)
                return ApiResponse<StripeCreateChekoutSessionResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<StripeCreateChekoutSessionResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<StripeCreateChekoutSessionResponse>();
                return data != null
                    ? ApiResponse<StripeCreateChekoutSessionResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<StripeCreateChekoutSessionResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<StripeCreateChekoutSessionResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}