namespace BookingAssistantWeb.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public User? User { get; set; }
    }

    public class PhoneNumbersResponse
    {
        public List<PhoneNumber> PhoneNumbers { get; set; } = new();
    }
    
     public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}