namespace BookingAssistantWeb.Services
{
    public class TokenService
    {
        private string? accessToken;

        public string? AccessToken
        {
            get => accessToken;
            set => accessToken = value;
        }

        public bool IsLoggedIn => !string.IsNullOrEmpty(accessToken);

        public void Clear() => accessToken = null;
    }

}