namespace BookingAssistantWeb.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
    }

    public class PhoneNumbersResponse
    {
        public List<PhoneNumber> PhoneNumbers { get; set; } = new();
    }
}