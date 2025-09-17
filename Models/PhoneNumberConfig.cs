namespace BookingAssistantWeb.Models
{
    public class PhoneNumberConfig
    {
        public int id { get; set; }
        public int bookingWindowSeconds { get; set; }
        public int bookingMarginSeconds { get; set; }
        public int bookingIntervalSeconds { get; set; }

        public TimeSpan BookingWindow => new(0, 0, bookingWindowSeconds);
        public TimeSpan BookingMargin => new(0, 0, bookingMarginSeconds);
        public TimeSpan BookingInterval => new(0, 0, bookingIntervalSeconds);
    }
}