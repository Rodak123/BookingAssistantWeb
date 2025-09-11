namespace BookingAssistantWeb.Services
{
    public class AppsettingsService
    {
        private string? apiServer;

        public string? ApiServer => apiServer;

        public void Clear()
        {
            apiServer = null;
        }

        public void Load(Dictionary<string, string> appsettings)
        {
            apiServer = appsettings["ApiServer"];
        }
    }

}