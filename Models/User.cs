public class User
{
    public int id { get; set; }
    public string email { get; set; } = string.Empty;
    public string givenName { get; set; } = string.Empty;
    public string familyName { get; set; } = string.Empty;
    public string? picture { get; set; }
    public string? locale { get; set; }
}