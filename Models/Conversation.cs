namespace BookingAssistantWeb.Models
{
    public class Conversation
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int durationSeconds { get; set; }
        public int messageCount { get; set; }
        public List<Message>? messages { get; set; }
    }
}