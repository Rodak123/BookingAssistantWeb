using System.Text.Json;
using Microsoft.JSInterop;

namespace BookingAssistantWeb.Services.Api
{
    public class ApiService
    {
        public readonly IJSRuntime JS;
        public readonly AppsettingsService AppsettingsService;

        public ApiService(IJSRuntime JS, AppsettingsService AppsettingsService)
        {
            this.JS = JS;
            this.AppsettingsService = AppsettingsService;
        }

        public async Task<JsApiResponse?> FetchApiPost(string endpoint, string json)
        {
            try
            {
                string raw = await JS.InvokeAsync<string>("fetchApi", AppsettingsService.ApiServer, endpoint, "POST", json);
                return JsonSerializer.Deserialize<JsApiResponse>(raw);
            }
            catch (Exception ex)
            {
                return new JsApiResponse
                {
                    Type = "error",
                    Status = 0,
                    Message = $"Interop error: {ex.Message}"
                };
            }
        }

        public async Task<JsApiResponse?> FetchApiGet(string endpoint)
        {
            try
            {
                string raw = await JS.InvokeAsync<string>("fetchApi", AppsettingsService.ApiServer, endpoint, "GET");
                return JsonSerializer.Deserialize<JsApiResponse>(raw);
            }
            catch (Exception ex)
            {
                return new JsApiResponse
                {
                    Type = "error",
                    Status = 0,
                    Message = $"Interop error: {ex.Message}"
                };
            }
        }
    }

}