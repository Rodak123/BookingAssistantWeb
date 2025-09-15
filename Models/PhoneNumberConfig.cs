namespace BookingAssistantWeb.Models
{
    public class PhoneNumberConfig
    {
        public int id { get; set; }
        public int bookingWindowSeconds { get; set; }
        public int bookingMarginSeconds { get; set; }
        public int bookingIntervalSeconds { get; set; }
    }
}