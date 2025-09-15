namespace BookingAssistantWeb.Services.Api
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public int? StatusCode { get; set; }

        public static ApiResponse<T> FromSuccess(T data, int? statusCode = null) =>
            new ApiResponse<T> { Success = true, Data = data, StatusCode = statusCode };

        public static ApiResponse<T> FromError(string? errorMessage, int? statusCode = null) =>
            new ApiResponse<T> { Success = false, ErrorMessage = errorMessage, StatusCode = statusCode };
    }
}