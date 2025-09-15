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

        public DateTime? EndDate => endDate == null ? null : DateTime.Parse(endDate);
        public DateTime StartDate => DateTime.Parse(startDate);

        public bool IsPaid => userSubscriptionId != null && endDate != null && EndDate > DateTime.UtcNow;
    }
}