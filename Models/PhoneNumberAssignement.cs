namespace BookingAssistantWeb.Models
{
    public class PhoneNumberAssignment
    {
        public int id { get; set; }

        public int userId { get; set; }
        public int phoneNumberId { get; set; }
        public int phoneNumberConfigId { get; set; }
        public int? userSubscriptionId { get; set; } = null;

        public string startDate { get; set; } = string.Empty;
        public string? endDate { get; set; } = null;
    }
}