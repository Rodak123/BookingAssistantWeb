namespace BookingAssistantWeb.Models
{
    public class Message
    {
        public int id { get; set; }

        public string content { get; set; } = string.Empty;
        public bool isFromBot { get; set; }
        public string date { get; set; } = string.Empty;
    }
}