public class TokenService
{
    private string? _accessToken;

    public string? AccessToken
    {
        get => _accessToken;
        set => _accessToken = value;
    }

    public bool IsLoggedIn => !string.IsNullOrEmpty(_accessToken);

    public void Clear() => _accessToken = null;
}
