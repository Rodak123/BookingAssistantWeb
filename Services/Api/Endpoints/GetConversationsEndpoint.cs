using System.Text.Json;
using BookingAssistantWeb.Models;

namespace BookingAssistantWeb.Services.Api.Endpoints
{
    public class GetConversationsResponse
    {
        public List<Conversation> conversations { get; set; } = new();
    }

    public static class GetConversationsEndpoint
    {
        public static async Task<ApiResponse<GetConversationsResponse>> FetchGetConversationsEndpoint(this ApiService apiService)
        {
            JsApiResponse? jsResponse = await apiService.FetchApiGet("/user/conversations");

            if (jsResponse == null)
                return ApiResponse<GetConversationsResponse>.FromError("No response from JS layer");

            if (jsResponse.Type == "error")
                return ApiResponse<GetConversationsResponse>.FromError(jsResponse.Message, jsResponse.Status);

            try
            {
                var data = jsResponse.Data?.Deserialize<GetConversationsResponse>();
                return data != null
                    ? ApiResponse<GetConversationsResponse>.FromSuccess(data, jsResponse.Status)
                    : ApiResponse<GetConversationsResponse>.FromError("Failed to deserialize Data");
            }
            catch (JsonException ex)
            {
                return ApiResponse<GetConversationsResponse>.FromError($"Invalid JSON: {ex.Message}");
            }
        }
    }
}