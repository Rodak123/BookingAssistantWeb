using System.Text.Json;
using BookingAssistantWeb.Models;

namespace BookingAssistantWeb.Services.Api.Endpoints
{
    public class GetConversationRequest
    {
        public int conversationId { get; set; }
    }

    public class GetConversationResponse
    {
        public Conversation? conversation { get; set; }
    }

    public static class GetConversationEndpoint
    {
        public static async Task<ApiResponse<GetConversationResponse>> FetchGetConversationEndpoint(this ApiService apiService, GetConversationRequest request)
        {
            string requestJson = JsonSerializer.Serialize(request);
            JsApiResponse? jsResponse = await apiService.FetchApiPost("/user/conversation", requestJson);

            if (jsResponse == null)
                return ApiResponse<GetConversationResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<GetConversationResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<GetConversationResponse>();
                return data != null
                    ? ApiResponse<GetConversationResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<GetConversationResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<GetConversationResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}